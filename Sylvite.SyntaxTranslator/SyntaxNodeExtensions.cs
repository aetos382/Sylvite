using Microsoft.CodeAnalysis.CSharp;

using TransportTextSpan = Sylvite.Transport.TextSpan;

namespace Sylvite.SyntaxTranslator;

internal static class SyntaxNodeExtensions
{
    public static TransportTextSpan GetSpan(
        this CSharpSyntaxNode node)
    {
        var span = node.GetLocation().SourceSpan;
        return new TransportTextSpan(span.Start, span.End);
    }
}
