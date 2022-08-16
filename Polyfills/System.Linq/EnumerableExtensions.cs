using System.Collections.Generic;

namespace System.Linq;

internal static class EnumerableExtensions
{
    public static IEnumerable<TSource[]> Chunk<TSource>(
        this IEnumerable<TSource> source,
        int size)
    {
        return new ChunkIterator<TSource>(source, size);
    }
}
