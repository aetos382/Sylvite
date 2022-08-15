using System;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.DebuggerVisualizers;

using Sylvite.Diagnostics;
using Sylvite.SyntaxTranslator;
using Sylvite.Transport;

namespace Sylvite.Debuggee;

public class SyntaxTransportSource :
    VisualizerObjectSource,
    ITransportRequestHandler
{
    public override void TransferData(
        object target,
        Stream incomingData,
        Stream outgoingData)
    {
        var command = GetDeserializableObject(incomingData).ToObject<ITransportRequest>();
        var context = new RequestContext(target, incomingData, outgoingData);

        command.Handle(this, context);
    }

    public void OnGetObject(
        GetObjectRequest command,
        RequestContext context)
    {
        Guard.NotNull(command);
        Guard.NotNull(context);

        var node = context.VisualizeTarget switch {
            CSharpSyntaxNode n => n,
            CSharpSyntaxTree t => t.GetCompilationUnitRoot(),
            _ => throw new NotSupportedException()
        };

        var nodeEnumerator = new SyntaxConverter(node);
        var transports = nodeEnumerator.Take(command.Count).ToArray();
        var response = new GetObjectResponse(transports, transports.Length < command.Count);

        context.SendObject(response);
    }
}
