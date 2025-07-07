using System.Runtime.CompilerServices;

namespace Foundation;

public static class AsyncEnumerableExtensions
{
    public static async IAsyncEnumerable<TItem> Skip<TItem>(this IAsyncEnumerable<TItem> @this, int count, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator = @this.GetAsyncEnumerator(cancellationToken);
        for (var i = 0; i < count; i++, await enumerator.MoveNextAsync())
        {
            yield return enumerator.Current;
        }
    }

    public static async IAsyncEnumerable<TItem> Take<TItem>(this IAsyncEnumerable<TItem> @this, int count, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var enumerator = @this.GetAsyncEnumerator(cancellationToken);
        for (var i = 0; i < count; i++, await enumerator.MoveNextAsync())
        {
            yield return enumerator.Current;
        }
    }

    public static async Task<TItem[]> ToArray<TItem>(this IAsyncEnumerable<TItem> @this, CancellationToken cancellationToken = default)
    {
        var list = new List<TItem>();

        var enumerator = @this.GetAsyncEnumerator(cancellationToken);
        while (await enumerator.MoveNextAsync())
        {
            list.Add(enumerator.Current);
        }

        return [.. list];
    }
}
