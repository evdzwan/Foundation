using Foundation.Threading;

namespace Foundation;

public static class AsyncValue
{
    public static IAsyncValue<TValue> Create<TValue>(Func<Task<TValue>> getValue)
        => new AsyncValue<TValue>(_ => getValue().AsITask());

    public static IAsyncValue<TValue> Create<TValue>(Func<ITask<TValue>> getValue)
        => new AsyncValue<TValue>(_ => getValue());

    public static IAsyncValue<TValue> Create<TValue>(Func<CancellationToken, Task<TValue>> getValue)
        => new AsyncValue<TValue>(cancellationToken => getValue(cancellationToken).AsITask());

    public static IAsyncValue<TValue> Create<TValue>(Func<CancellationToken, ITask<TValue>> getValue)
        => new AsyncValue<TValue>(getValue);
}
