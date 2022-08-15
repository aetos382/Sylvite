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
        public override VisitResult<SyntaxTransport>? VisitCompilationUnit(
            CompilationUnitSyntax node)
        {
            var transport = new CompilationUnitTransport(
                node.GetSpan(),
                null,
                0);

            var childNodes = node
                .ChildNodes()
                .Cast<CSharpSyntaxNode>();

            return new VisitResult<SyntaxTransport>(transport, childNodes);
        }
    }
}
