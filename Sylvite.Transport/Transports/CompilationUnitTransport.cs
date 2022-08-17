using System;

using CommunityToolkit.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public class CompilationUnitTransport :
    SyntaxTransport
{
    public override T Accept<T>(
        ITransportVisitor<T> visitor)
    {
        Guard.IsNotNull(visitor);

        return visitor.VisitCompilationUnit(this);
    }

    public CompilationUnitTransport(
        TextSpan span,
        Guid? parentId,
        int depth)
        : base(
            span,
            parentId,
            depth)
    {
    }
}
