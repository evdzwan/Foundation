namespace Foundation;

public interface IAsyncValue<TValue>
{
    Task<TValue> GetValue(CancellationToken cancellationToken = default);
    void Reset();
}
