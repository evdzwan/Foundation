namespace Foundation;

sealed class AsyncValue<TValue>(Func<CancellationToken, Task<TValue>> getValue) : IAsyncValue<TValue>
{
    TValue? Value;

    public async Task<TValue> GetValue(CancellationToken cancellationToken = default)
        => Value ??= await getValue(cancellationToken);

    public void Reset()
        => Value = default;
}
