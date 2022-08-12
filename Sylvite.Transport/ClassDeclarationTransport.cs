using System;
using System.Collections.Generic;

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
        return visitor.VisitClassDeclaration(this);
    }
}
