using System.Runtime.CompilerServices;

namespace Foundation.Threading;

[AsyncMethodBuilder(typeof(TaskMethodBuilder<>))]
public interface ITask<out TResult> : ITask
{
    TResult Result { get; }

    IConfiguredTask ITask.ConfigureAwait(bool continueOnCapturedContext) => ConfigureAwait(continueOnCapturedContext);
    new IConfiguredTask<TResult> ConfigureAwait(bool continueOnCapturedContext);

    ITaskAwaiter ITask.GetAwaiter() => GetAwaiter();
    new ITaskAwaiter<TResult> GetAwaiter();
}
