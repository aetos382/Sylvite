#if ROSLYN_4_0_1_OR_GREATER

using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.Transport;

namespace Sylvite.SyntaxTranslator;

public partial class SyntaxConverter
{
    public override SyntaxTransport? VisitFileScopedNamespaceDeclaration(
        FileScopedNamespaceDeclarationSyntax node)
    {
        return this.ProcessNode(
            node,
            static (node, parent) =>
                new FileScopedNamespaceDeclarationTransport(
                    node.Name.ToString(),
                    node.GetSpan(),
                    parent),
            static (self, node, transport) =>
                node.ChildNodes().Select(self.Visit));
    }
}

#endif
