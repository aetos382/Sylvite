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
