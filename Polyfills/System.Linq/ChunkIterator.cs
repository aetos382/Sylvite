using System.Collections;
using System.Collections.Generic;

namespace System.Linq;

internal class ChunkIterator<TSource>
    : IEnumerable<TSource[]>
{
    public ChunkIterator(
        IEnumerable<TSource> source,
        int count)
    {
        this._source = source;
        this._count = count;
    }

    public IEnumerator<TSource[]> GetEnumerator()
    {
        var count = this._count;
        using var enumerator = this._source.GetEnumerator();

        while (true)
        {
            var array = new TSource[count];

            for (int i = 0; i < count; ++i)
            {
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                array[i] = enumerator.Current;
            }

            yield return array;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private readonly IEnumerable<TSource> _source;

    private readonly int _count;
}