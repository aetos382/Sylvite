using System;
using System.IO;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace Sylvite.Debuggee;

public class SyntaxTransportSource :
    VisualizerObjectSource
{
    public override void TransferData(
        object target,
        Stream incomingData,
        Stream outgoingData)
    {
        base.TransferData(target, incomingData, outgoingData);
    }

    public override void GetData(
        object target,
        Stream outgoingData)
    {
        if (target is CSharpSyntaxTree tree)
        {
            var rootNode = tree.GetCompilationUnitRoot();
            this.GetSyntaxNodeData(rootNode, outgoingData);
            return;
        }
        else if (target is CSharpSyntaxNode node)
        {
            this.GetSyntaxNodeData(node, outgoingData);
            return;
        }

        throw new NotSupportedException();
    }

    private void GetSyntaxNodeData(
        CSharpSyntaxNode node,
        Stream outgoingData)
    {
        var converter = new SyntaxConverter();
        var transport = converter.Visit(node);

        base.GetData(transport, outgoingData);
    }
}
