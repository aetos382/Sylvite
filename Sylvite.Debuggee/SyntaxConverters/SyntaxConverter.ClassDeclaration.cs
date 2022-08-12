using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.Transport;

namespace Sylvite.Debuggee;

internal partial class SyntaxConverter
{
    public override SyntaxTransport VisitClassDeclaration(
        ClassDeclarationSyntax node)
    {
        var name = node.Identifier.ValueText;

        var transport = new ClassDeclarationTransport(
            name);

        this._onTraverse?.Invoke(transport);

        return transport;
    }
}
