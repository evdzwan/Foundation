using Foundation.Protocol;
using Foundation.Threading;

namespace Foundation.Collections;

public static class AsyncCollection
{
    public static IAsyncCollection<TItem> Create<TItem>(Func<Query, Task<TItem[]>> getView)
        => new AsyncCollection<TItem>((query, _) => getView(query).AsITask());

    public static IAsyncCollection<TItem> Create<TItem>(Func<Query, ITask<TItem[]>> getView)
        => new AsyncCollection<TItem>((query, _) => getView(query));

    public static IAsyncCollection<TItem> Create<TItem>(Func<Query, CancellationToken, Task<TItem[]>> getView)
        => new AsyncCollection<TItem>((query, cancellationToken) => getView(query, cancellationToken).AsITask());

    public static IAsyncCollection<TItem> Create<TItem>(Func<Query, CancellationToken, ITask<TItem[]>> getView)
        => new AsyncCollection<TItem>(getView);
}
