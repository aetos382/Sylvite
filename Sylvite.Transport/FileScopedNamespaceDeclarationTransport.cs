using System;
using System.Collections.Generic;

namespace Sylvite.Transport;

[Serializable]
public class FileScopedNamespaceDeclarationTransport :
    BaseNamespaceDeclarationTransport
{
    public FileScopedNamespaceDeclarationTransport(
        string name)
        : base(
            name)
    {
    }

    public override T Accept<T>(
        ITransportVisitor<T> visitor)
    {
        return visitor.VisitFileScopedNamespaceDeclaration(this);
    }
}
