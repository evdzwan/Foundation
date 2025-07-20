using Foundation.Threading;

namespace Foundation;

public interface IAsyncValue<out TValue>
{
    ITask<TValue> GetValue(CancellationToken cancellationToken = default);
    void Reset();
}
