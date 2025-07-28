namespace Foundation;

public interface IAsyncValue<TValue>
{
    Task<TValue> GetValue(CancellationToken cancellationToken = default);
    void Reset();

    IDisposable Subscribe(Action<IAsyncValue<TValue>> resetHandler);
}
