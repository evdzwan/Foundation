using Foundation.Threading;

namespace Foundation;

sealed class AsyncValue<TValue>(Func<CancellationToken, ITask<TValue>> getValue) : IAsyncValue<TValue>
{
    TValue? Value;

    public async ITask<TValue> GetValue(CancellationToken cancellationToken = default)
        => Value ??= await getValue(cancellationToken);

    public void Reset()
        => Value = default;
}
