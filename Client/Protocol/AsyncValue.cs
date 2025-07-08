namespace Foundation.Protocol;

public static class AsyncValue
{
    public static IAsyncValue<TValue> Create<TValue>(Func<CancellationToken, Task<TValue>> resolve) where TValue : notnull
        => new RelayAsyncValue<TValue>(resolve);

    sealed class RelayAsyncValue<TValue>(Func<CancellationToken, Task<TValue>> resolve) : IAsyncValue<TValue> where TValue : notnull
    {
        TValue? Value;

        public async Task<TValue> GetValue(CancellationToken cancellationToken = default)
            => Value ??= await resolve(cancellationToken);
    }
}
