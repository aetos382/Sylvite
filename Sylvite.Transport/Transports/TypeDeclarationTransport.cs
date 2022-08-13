using System;

namespace Sylvite.Transport;

[Serializable]
public abstract class TypeDeclarationTransport :
    SyntaxTransport
{
    public string Name { get; }

    protected TypeDeclarationTransport(
        string name,
        TextSpan span,
        SyntaxTransport? parent)
        : base(
            span,
            parent)
    {
        this.Name = name;
    }
}
