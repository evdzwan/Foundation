namespace Foundation;

public interface IAsyncValue<TValue> where TValue : notnull
{
    Task<TValue> GetValue(CancellationToken cancellationToken = default);
}
