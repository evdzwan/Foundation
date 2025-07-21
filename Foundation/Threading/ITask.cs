using System.Runtime.CompilerServices;

namespace Foundation.Threading;

[AsyncMethodBuilder(typeof(TaskMethodBuilder))]
public interface ITask
{
    IConfiguredTask ConfigureAwait(bool continueOnCapturedContext);
    ITaskAwaiter GetAwaiter();
}
