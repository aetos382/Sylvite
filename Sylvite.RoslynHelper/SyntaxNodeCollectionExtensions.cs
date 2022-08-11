using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Sylvite.RoslynHelper;

public static class SyntaxNodeCollectionExtensions
{
    public static SyntaxList<T> ToSyntaxList<T>(
        this IEnumerable<T> nodes)
        where T :
            CSharpSyntaxNode
    {
        return SyntaxFactory.List(nodes);
    }
}
