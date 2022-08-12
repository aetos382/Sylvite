using System;

using Sylvie.Diagnostics;

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
        Guard.NotNull(visitor);

        return visitor.VisitFileScopedNamespaceDeclaration(this);
    }
}
