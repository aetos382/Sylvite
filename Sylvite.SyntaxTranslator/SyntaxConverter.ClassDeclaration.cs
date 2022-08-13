using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.Transport;

namespace Sylvite.SyntaxTranslator;

public partial class SyntaxConverter
{
    public override SyntaxTransport? VisitClassDeclaration(
        ClassDeclarationSyntax node)
    {
        return this.ProcessNode(
            node,
            static (node, parent) =>
                new ClassDeclarationTransport(
                    node.Identifier.ValueText,
                    node.GetSpan(),
                    parent),
            static (self, node, transport) =>
                node.ChildNodes().Select(self.Visit));
    }
}
