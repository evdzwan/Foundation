namespace Foundation;

public static class AsyncValue
{
    public static IAsyncValue<TValue> Create<TValue>(Func<CancellationToken, Task<TValue>> resolve)
        => new AsyncValue<TValue>(resolve);
}
