using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.Transport;

namespace Sylvite.Debuggee;

internal partial class SyntaxConverter
{
    public override SyntaxTransport VisitClassDeclaration(
        ClassDeclarationSyntax node)
    {
        var name = node.Identifier.ValueText;

        return new ClassDeclarationTransport(
            name);
    }
}
