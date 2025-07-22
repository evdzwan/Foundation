using Foundation.Protocol;

namespace Foundation.Collections;

public static class AsyncCollection
{
    public static IAsyncCollection<TItem> Create<TItem>(IEnumerable<TItem> collection)
        => new AsyncCollection<TItem>((page, _) => Task.FromResult(collection.Skip(page.Skip).Take(page.Take).ToArray()));

    public static IAsyncCollection<TItem> Create<TItem>(Func<Page, Task<TItem[]>> getView)
        => new AsyncCollection<TItem>((page, _) => getView(page));

    public static IAsyncCollection<TItem> Create<TItem>(Func<Page, CancellationToken, Task<TItem[]>> getView)
        => new AsyncCollection<TItem>(getView);
}
