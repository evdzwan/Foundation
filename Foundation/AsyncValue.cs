namespace Foundation;

public static class AsyncValue
{
    public static IAsyncValue<TValue> Create<TValue>(TValue value)
        => new AsyncValue<TValue>(_ => Task.FromResult(value));

    public static IAsyncValue<TValue> Create<TValue>(Func<Task<TValue>> getValue)
        => new AsyncValue<TValue>(_ => getValue());

    public static IAsyncValue<TValue> Create<TValue>(Func<CancellationToken, Task<TValue>> getValue)
        => new AsyncValue<TValue>(getValue);
}
