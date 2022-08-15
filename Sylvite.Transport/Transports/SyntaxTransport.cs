using System;

using Sylvite.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public abstract class SyntaxTransport
{
    protected SyntaxTransport(
        TextSpan span,
        Guid? parentId,
        int depth)
    {
        Guard.GreaterThanOrEqualTo(depth, 0);

        this.Id = Guid.NewGuid();
        this.Span = span;
        this.ParentId = parentId;
        this.Depth = depth;
    }

    public TextSpan Span { get; }

    public Guid Id { get; }

    public Guid? ParentId { get; }

    public int Depth { get; }

    public abstract T Accept<T>(
        ITransportVisitor<T> visitor);
}
