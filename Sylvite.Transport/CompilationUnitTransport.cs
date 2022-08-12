using System;

using Sylvie.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public class CompilationUnitTransport :
    SyntaxTransport
{
    public override T Accept<T>(
        ITransportVisitor<T> visitor)
    {
        Guard.NotNull(visitor);

        return visitor.VisitCompilationUnit(this);
    }
}
