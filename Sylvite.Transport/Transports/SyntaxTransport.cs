using System;
using System.Collections.Generic;

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

    public SyntaxTransport? Parent { get; private set; }

    public int Depth { get; }

    public void SetParent(
        SyntaxTransport parent,
        bool setChild)
    {
        this.Parent = parent;

        if (setChild)
        {
            parent.AddChild(this, false);
        }
    }

    public void AddChild(
        SyntaxTransport child,
        bool setParent)
    {
        this._childNodes.Add(child);

        if (setParent)
        {
            child.SetParent(this, false);
        }
    }

    private readonly List<SyntaxTransport> _childNodes = new();

    public IReadOnlyList<SyntaxTransport> ChildNodes
    {
        get
        {
            return this._childNodes.AsReadOnly();
        }
    }

    public abstract T Accept<T>(
        ITransportVisitor<T> visitor);
}
