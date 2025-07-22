using Foundation.Protocol;

namespace Foundation.Collections;

public interface IAsyncCollection<TItem> : IAsyncEnumerable<TItem>, IAsyncValue<TItem[]>
{
    bool Complete { get; }
    int Count { get; }

    Task<TItem[]> GetView(Page page, CancellationToken cancellationToken = default);
}
