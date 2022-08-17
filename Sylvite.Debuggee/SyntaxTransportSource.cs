using System.IO;

using Microsoft.VisualStudio.DebuggerVisualizers;

using CommunityToolkit.Diagnostics;

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

        var response = command.Handle(this, context);
        Serialize(outgoingData, response);
    }

    public GetObjectResponse HandleGetObject(
        GetObjectRequest command,
        RequestContext context)
    {
        Guard.IsNotNull(command);
        Guard.IsNotNull(context);

        return new GetObjectResponse();
    }
}
