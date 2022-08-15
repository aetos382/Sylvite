using System;

using Sylvite.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public class ClassDeclarationTransport :
    TypeDeclarationTransport
{
    public ClassDeclarationTransport(
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
        Guard.NotNull(visitor);

        return visitor.VisitClassDeclaration(
            this);
    }
}
