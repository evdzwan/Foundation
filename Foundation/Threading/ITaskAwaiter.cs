using System.Runtime.CompilerServices;

namespace Foundation.Threading;

public interface ITaskAwaiter : ICriticalNotifyCompletion
{
    bool IsCompleted { get; }
}
