using System;

using Sylvie.Diagnostics;

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
        Guard.NotNull(visitor);

        return visitor.VisitNamespaceDeclaration(this);
    }
}
