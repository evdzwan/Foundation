namespace Foundation;

sealed class AsyncValue<TValue>(Func<CancellationToken, Task<TValue>> getValue) : IAsyncValue<TValue>
{
    Task<TValue>? ValueTask;

    public async Task<TValue> GetValue(CancellationToken cancellationToken = default)
        => await (ValueTask ??= getValue(cancellationToken));

    public void Reset()
        => ValueTask = null;
}
