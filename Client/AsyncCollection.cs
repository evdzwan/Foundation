namespace Foundation;

public static class AsyncCollection
{
    public static IAsyncCollection<TItem> Create<TItem>(Func<Transform, CancellationToken, Task<TItem[]>> resolve)
        => new AsyncCollection<TItem>(resolve);
}
