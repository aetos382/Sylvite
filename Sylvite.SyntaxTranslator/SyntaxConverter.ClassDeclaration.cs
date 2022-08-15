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
        public override VisitResult<SyntaxTransport>? VisitClassDeclaration(
            ClassDeclarationSyntax node)
        {
            var transport = new ClassDeclarationTransport(
                node.Identifier.ValueText,
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
