using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.Transport;

namespace Sylvite.Debuggee;

internal partial class SyntaxConverter
{
    public override SyntaxTransport VisitFileScopedNamespaceDeclaration(
        FileScopedNamespaceDeclarationSyntax node)
    {
        return new FileScopedNamespaceDeclarationTransport(
            node.Name.ToString());
    }
}
