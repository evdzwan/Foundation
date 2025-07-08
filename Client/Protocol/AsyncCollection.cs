using Foundation;
using System.Collections.Concurrent;

namespace Foundation.Protocol;

public static class AsyncCollection
{
    const int DefaultViewSize = 20;

    public static IAsyncCollection<TItem> Create<TItem>(Func<Query, CancellationToken, Task<TItem[]>> resolve) where TItem : notnull
        => new RelayAsyncCollection<TItem>(DefaultViewSize, resolve);

    sealed class RelayAsyncCollection<TItem>(int viewSize, Func<Query, CancellationToken, Task<TItem[]>> resolve) : IAsyncCollection<TItem> where TItem : notnull
    {
        readonly ConcurrentDictionary<int, Task<TItem[]>> Views = [];

        public bool Complete { get; private set; }

        public int Count
        {
            get
            {
                if (Views.IsEmpty)
                {
                    return 0;
                }

                var maxWindowKey = Views.Keys.Max();
                return maxWindowKey * viewSize + Views[maxWindowKey].Result.Length + (Complete ? 0 : viewSize);
            }
        }

        public async IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            for (var i = 0; ; i++)
            {
                var items = await GetView(new(Page: new(Skip: i * viewSize, Take: viewSize)), cancellationToken);
                if (items.Length == 0)
                {
                    yield break;
                }

                foreach (var item in items)
                {
                    yield return item;
                }
            }
        }

        public Task<TItem[]> GetValue(CancellationToken cancellationToken = default)
            => this.ToArray(cancellationToken);

        public async Task<TItem[]> GetView(Query query, CancellationToken cancellationToken = default)
        {
            var skip = query.Page?.Skip ?? 0;
            var take = query.Page?.Take ?? viewSize;

            var windowStart = (int)Math.Floor(skip / (viewSize * 1d));
            var windowCount = (int)Math.Ceiling(take / (viewSize * 1d));

            var items = new List<TItem>();
            foreach (var i in Enumerable.Range(windowStart, windowCount))
            {
                var windowItems = await Views.GetOrAdd(i, j => resolve(query with { Page = new(Skip: j * viewSize, Take: viewSize) }, cancellationToken));
                items.AddRange(windowItems);

                if (windowItems.Length < viewSize)
                {
                    Complete = true;
                    break;
                }
            }

            return [.. items.Skip(skip - windowStart * viewSize).Take(take)];
        }

        public void Reset()
        {
            Complete = false;
            Views.Clear();
        }
    }
}
