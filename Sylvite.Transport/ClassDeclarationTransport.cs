using System;

using Sylvie.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public class ClassDeclarationTransport :
    TypeDeclarationTransport
{
    public ClassDeclarationTransport(
        string name)
        : base(
            name)
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
