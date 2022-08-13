using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.Transport;

namespace Sylvite.SyntaxTranslator;

public partial class SyntaxConverter
{
    public override SyntaxTransport? VisitCompilationUnit(
        CompilationUnitSyntax node)
    {
        return this.ProcessNode(
            node,
            static (node, _) =>
                new CompilationUnitTransport(
                    node.GetSpan()),
            static (self, node, transport) =>
                node.ChildNodes().Select(self.Visit));
    }
}
