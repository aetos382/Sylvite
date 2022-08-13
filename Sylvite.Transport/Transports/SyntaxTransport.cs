using System;
using System.Collections.Generic;

namespace Sylvite.Transport;

[Serializable]
public abstract class SyntaxTransport
{
    protected SyntaxTransport(
        TextSpan span,
        SyntaxTransport? parent)
    {
        this.Id = Guid.NewGuid();
        this.Span = span;
        this.Parent = parent;
        this.Depth = (parent?.Depth + 1) ?? 0;
    }

    public TextSpan Span { get; }

    public SyntaxTransport? Parent { get; }

    public int Depth { get; }

    private readonly List<SyntaxTransport> _children = new();

    public void AddChild(
        SyntaxTransport child)
    {
        this._children.Add(child);
    }

    public IReadOnlyList<SyntaxTransport> Children
    {
        get
        {
            return this._children.AsReadOnly();
        }
    }

    public Guid Id { get; }

    public abstract T Accept<T>(
        ITransportVisitor<T> visitor);
}
