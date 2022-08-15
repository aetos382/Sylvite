using System;

namespace Sylvite.Transport;

[Serializable]
public abstract class BaseNamespaceDeclarationTransport :
    SyntaxTransport
{
    public string Name { get; }

    protected BaseNamespaceDeclarationTransport(
        string name,
        TextSpan span,
        Guid? parentId,
        int depth)
        : base(
            span,
            parentId,
            depth)
    {
        this.Name = name;
    }
}
