using Foundation.Protocol;
using Foundation.Threading;

namespace Foundation.Collections;

public interface IAsyncCollection<out TItem> : IAsyncEnumerable<TItem>, IAsyncValue<TItem[]>
{
    bool Complete { get; }
    int Count { get; }

    ITask<TItem[]> GetView(Query query, CancellationToken cancellationToken = default);
}
