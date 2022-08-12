using System;
using System.Collections.Generic;

namespace Sylvite.Transport;

[Serializable]
public class CompilationUnitTransport :
    SyntaxTransport
{
    public override T Accept<T>(
        ITransportVisitor<T> visitor)
    {
        return visitor.VisitCompilationUnit(this);
    }
}
