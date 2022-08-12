using System;

namespace Sylvite.Transport;

[Serializable]
public abstract class TypeDeclarationTransport :
    SyntaxTransport
{
    public string Name { get; }

    protected TypeDeclarationTransport(
        string name)
    {
        this.Name = name;
    }
}
