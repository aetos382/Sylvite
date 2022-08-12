using System;
using System.Collections.Generic;

namespace Sylvite.Transport;

[Serializable]
public abstract class BaseNamespaceDeclarationTransport :
    SyntaxTransport
{
    public string Name { get; }

    protected BaseNamespaceDeclarationTransport(
        string name)
    {
        Name = name;
    }
}
