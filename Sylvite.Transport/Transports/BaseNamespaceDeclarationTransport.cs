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
        SyntaxTransport? parent)
        : base(
            span,
            parent)
    {
        Name = name;
    }
}
