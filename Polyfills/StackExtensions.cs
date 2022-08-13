using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Collections.Generic;

internal static class StackExtensions
{
    public static bool TryPeek<T>(
        this Stack<T> stack,
        [MaybeNullWhen(false)] out T item)
    {
        item = default;

        if (!stack.Any())
        {
            return false;
        }

        item = stack.Peek();
        return true;
    }
}
