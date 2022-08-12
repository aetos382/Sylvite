using System;
using System.Collections.Generic;

namespace Sylvite.Transport;

[Serializable]
public abstract class SyntaxTransport
{
    protected SyntaxTransport()
    {
    }

    private readonly List<SyntaxTransport> _children = new();

    protected void AddChild(
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

    public Guid Id { get; } = Guid.NewGuid();

    public abstract T Accept<T>(
        ITransportVisitor<T> visitor);
}
