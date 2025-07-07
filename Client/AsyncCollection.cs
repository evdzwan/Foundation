using Foundation.Protocol;
using System.Collections.Concurrent;

namespace Foundation;

public static class AsyncCollection
{
    public static IAsyncCollection<TItem> Create<TItem>(Func<Query, CancellationToken, Task<TItem[]>> resolve) where TItem : notnull
        => new RelayAsyncCollection<TItem>(resolve);

    sealed class RelayAsyncCollection<TItem>(Func<Query, CancellationToken, Task<TItem[]>> resolve) : IAsyncCollection<TItem> where TItem : notnull
    {
        readonly ConcurrentDictionary<Query, Task<TItem[]>> Views = [];

        public async IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            var query = new Query(Page: new(Skip: 0, Take: 20));
            for (var i = 0; ; i++)
            {
                var items = await GetView(query with { Page = query.Page! with { Skip = i * query.Page.Take } }, cancellationToken);
                if (items.Length == 0)
                {
                    yield break;
                }

                foreach (var item in items)
                {
                    yield return item;
                }

                //TODO anders oplossen
                if (items.Length < query.Page.Take)
                {
                    yield break;
                }
            }
        }

        public Task<TItem[]> GetValue(CancellationToken cancellationToken = default)
            => this.ToArray(cancellationToken);

        public Task<TItem[]> GetView(Query query, CancellationToken cancellationToken = default)
            => Views.GetOrAdd(query, t => resolve(query, cancellationToken));
    }
}
