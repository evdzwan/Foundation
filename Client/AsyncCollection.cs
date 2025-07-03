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

        public IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<TItem[]> GetView(Query query, CancellationToken cancellationToken = default)
            => Views.GetOrAdd(query, t => resolve(query, cancellationToken));
    }
}
