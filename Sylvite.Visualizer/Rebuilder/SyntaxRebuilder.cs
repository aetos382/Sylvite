using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp;

using Sylvite.Transport;

namespace Sylvite.Visualizer.Rebuilder;

internal class SyntaxRebuilder
{
    public CSharpSyntaxNode BuildNode(
        IReadOnlyList<SyntaxTransport> transports)
    {
        var dic = new Dictionary<Guid, SyntaxTransport>();

        SyntaxTransport? rootTransport = null;

        foreach (var transport in transports)
        {
            if (transport.ParentId is {} pid)
            {
                if (dic.TryGetValue(pid, out var parent))
                {
                    transport.SetParent(parent, true);
                }
            }
            else
            {
                rootTransport = transport;
            }

            dic[transport.Id] = transport;
        }

        var visitor = new SyntaxTransportVisitor();
        var rootNode = rootTransport.Accept(visitor);

        return rootNode;
    }
}
