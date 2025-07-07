using Foundation.Protocol;

namespace Foundation;

public interface IAsyncCollection<TItem> : IAsyncEnumerable<TItem>, IAsyncValue<TItem[]> where TItem : notnull
{
    bool Complete { get; }
    int Count { get; }

    Task<TItem[]> GetView(Query query, CancellationToken cancellationToken = default);
    void Reset();
}

