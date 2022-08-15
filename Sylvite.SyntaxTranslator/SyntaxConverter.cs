using Microsoft.CodeAnalysis.CSharp;

using Sylvite.RoslynHelper;
using Sylvite.Transport;

namespace Sylvite.SyntaxTranslator;

public partial class SyntaxConverter :
    SyntaxEnumerator<SyntaxTransport>
{
    public SyntaxConverter(
        CSharpSyntaxNode initialElement)
        : base(
            initialElement)
    {
    }

    protected override InternalVisitor CreateVisitor()
    {
        return new Visitor(this);
    }

    private partial class Visitor :
        InternalVisitor
    {
        public Visitor(
            SyntaxEnumerator<SyntaxTransport> visitor)
            : base(
                visitor)
        {
        }
    }
}
