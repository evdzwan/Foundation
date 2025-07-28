namespace Foundation;

sealed class AsyncValue<TValue>(Func<CancellationToken, Task<TValue>> getValue) : IAsyncValue<TValue>
{
    readonly List<Action<IAsyncValue<TValue>>> ResetHandlers = [];
    readonly Lock UpdateHandlersLock = new();
    Task<TValue>? ValueTask;

    public async Task<TValue> GetValue(CancellationToken cancellationToken = default)
        => await (ValueTask ??= getValue(cancellationToken));

    public void Reset()
    {
        ValueTask = null;
        NotifyResetHandlers();
    }

    public IDisposable Subscribe(Action<IAsyncValue<TValue>> resetHandler)
    {
        using (UpdateHandlersLock.EnterScope())
        {
            ResetHandlers.Add(resetHandler);
        }

        return new Disposable(() =>
        {
            using (UpdateHandlersLock.EnterScope())
            {
                ResetHandlers.Remove(resetHandler);
            }
        });
    }

    void NotifyResetHandlers()
    {
        using (UpdateHandlersLock.EnterScope())
        {
            ResetHandlers.ForEach(handler => handler(this));
        }
    }
}
