using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Sylvite.Transport;

namespace Sylvite.Debuggee;

internal partial class SyntaxConverter :
    CSharpSyntaxVisitor<SyntaxTransport>
{
    private readonly SyntaxWalkerDepth _depth;

    public SyntaxConverter(
        SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node)
    {
        this._depth = depth;
    }
}
