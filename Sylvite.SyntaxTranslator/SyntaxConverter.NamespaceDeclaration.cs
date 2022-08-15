using System.Linq;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.RoslynHelper;
using Sylvite.Transport;

namespace Sylvite.SyntaxTranslator;

public partial class SyntaxConverter
{
    private partial class Visitor
    {
        public override VisitResult<SyntaxTransport>? VisitNamespaceDeclaration(
            NamespaceDeclarationSyntax node)
        {
            var transport = new NamespaceDeclarationTransport(
                node.Name.ToString(),
                node.GetSpan(),
                this.Parent!.Id,
                this.Depth);

            var childNodes = node
                .ChildNodes()
                .Cast<CSharpSyntaxNode>();

            return new VisitResult<SyntaxTransport>(transport, childNodes);
        }
    }
}
