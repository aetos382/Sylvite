using System;
using System.Collections.Generic;

namespace Sylvite.Transport;

[Serializable]
public abstract class SyntaxTransport
{
    public IReadOnlyList<SyntaxTransport> Children { get; }

    protected SyntaxTransport(
        IReadOnlyList<SyntaxTransport>? children)
    {
        this.Children = children ?? Array.Empty<SyntaxTransport>();
    }

    public abstract T Accept<T>(
        ITransportVisitor<T> visitor);
}
