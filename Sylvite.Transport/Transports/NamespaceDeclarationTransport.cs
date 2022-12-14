using System;

using CommunityToolkit.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public class NamespaceDeclarationTransport :
    BaseNamespaceDeclarationTransport
{
    public NamespaceDeclarationTransport(
        string name,
        TextSpan span,
        Guid? parentId,
        int depth)
        : base(
            name,
            span,
            parentId,
            depth)
    {
    }

    public override T Accept<T>(
        ITransportVisitor<T> visitor)
    {
        Guard.IsNotNull(visitor);

        return visitor.VisitNamespaceDeclaration(this);
    }
}
