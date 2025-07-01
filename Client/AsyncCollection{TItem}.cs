using System.Collections.Concurrent;

namespace Foundation;

sealed class AsyncCollection<TItem>(Func<Transform, CancellationToken, Task<TItem[]>> resolve) : IAsyncCollection<TItem>
{
    readonly ConcurrentDictionary<Transform, Task<TItem[]>> Views = [];

    public IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TItem[]> GetView(Transform transform, CancellationToken cancellationToken = default)
        => Views.GetOrAdd(transform, t => resolve(transform, cancellationToken));
}
