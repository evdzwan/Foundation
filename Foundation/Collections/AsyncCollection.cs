using Foundation.Protocol;

namespace Foundation.Collections;

public static class AsyncCollection
{
    const int DefaultWindowSize = 50;

    public static IAsyncCollection<TItem> Create<TItem>(IEnumerable<TItem> collection, int windowSize = DefaultWindowSize)
        => new AsyncCollection<TItem>((page, _) => Task.FromResult(collection.Skip(page.Skip).Take(page.Take).ToArray()), windowSize);

    public static IAsyncCollection<TItem> Create<TItem>(Func<Page, Task<TItem[]>> getView, int windowSize = DefaultWindowSize)
        => new AsyncCollection<TItem>((page, _) => getView(page), windowSize);

    public static IAsyncCollection<TItem> Create<TItem>(Func<Page, CancellationToken, Task<TItem[]>> getView, int windowSize = DefaultWindowSize)
        => new AsyncCollection<TItem>(getView, windowSize);
}
