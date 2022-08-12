using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Sylvite.Transport;

namespace Sylvite.Debuggee;

internal partial class SyntaxConverter :
    CSharpSyntaxVisitor<SyntaxTransport>
{
    private readonly Action<SyntaxTransport>? _onTraverse;

    private readonly SyntaxWalkerDepth _depth;

    public SyntaxConverter(
        Action<SyntaxTransport>? onTraverse = null,
        SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node)
    {
        this._onTraverse = onTraverse;
        this._depth = depth;
    }
}
