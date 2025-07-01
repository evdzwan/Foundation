namespace Foundation;

sealed class AsyncValue<TValue>(Func<CancellationToken, Task<TValue>> resolve) : IAsyncValue<TValue>
{
    TValue? Value;

    public async Task<TValue> GetValue(CancellationToken cancellationToken = default)
        => Value ??= await resolve(cancellationToken);
}
