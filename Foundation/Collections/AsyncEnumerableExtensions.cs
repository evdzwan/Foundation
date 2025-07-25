﻿namespace Foundation.Collections;

public static class AsyncEnumerableExtensions
{
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
