using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.Transport;

namespace Sylvite.Debuggee;

internal partial class SyntaxConverter
{
    public override SyntaxTransport VisitNamespaceDeclaration(
        NamespaceDeclarationSyntax node)
    {
        return new NamespaceDeclarationTransport(
            node.Name.ToString(),
            null);
    }
}
