using Foundation.Protocol;

namespace Foundation.Collections;

public static class AsyncCollection
{
    public static IAsyncCollection<TItem> Create<TItem>(Func<Query, Task<TItem[]>> getView)
        => new AsyncCollection<TItem>((query, _) => getView(query));

    public static IAsyncCollection<TItem> Create<TItem>(Func<Query, CancellationToken, Task<TItem[]>> getView)
        => new AsyncCollection<TItem>(getView);
}
