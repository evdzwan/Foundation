using Foundation.Protocol;

namespace Foundation;

public interface IAsyncCollection<TItem> : IAsyncEnumerable<TItem> where TItem : notnull
{
    Task<TItem[]> GetView(Query query, CancellationToken cancellationToken = default);
}
