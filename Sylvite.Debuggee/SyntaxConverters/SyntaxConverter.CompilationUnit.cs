using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.Transport;

namespace Sylvite.Debuggee;

internal partial class SyntaxConverter
{
    public override SyntaxTransport VisitCompilationUnit(
        CompilationUnitSyntax node)
    {
        var childTransports = new List<SyntaxTransport>();

        foreach (var childNode in node.ChildNodes())
        {
            var childTransport = this.Visit(childNode);

            if (childTransport is not null)
            {
                childTransports.Add(childTransport);
            }
        }

        return new CompilationUnitTransport(childTransports);
    }
}
