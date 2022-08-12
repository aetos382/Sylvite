using System;
using System.Collections.Generic;

namespace Sylvite.Transport;

[Serializable]
public class NamespaceDeclarationTransport :
    BaseNamespaceDeclarationTransport
{
    public NamespaceDeclarationTransport(
        string name)
        : base(
            name)
    {
    }

    public override T Accept<T>(
        ITransportVisitor<T> visitor)
    {
        return visitor.VisitNamespaceDeclaration(this);
    }
}
