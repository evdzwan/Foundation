using System.Runtime.CompilerServices;

namespace Foundation.Threading;

sealed class TaskProxy(Task task) : ITask
{
    public IConfiguredTask ConfigureAwait(bool continueOnCapturedContext) => new ConfiguredTask(task, continueOnCapturedContext);
    public ITaskAwaiter GetAwaiter() => new TaskAwaiterProxy(task.GetAwaiter());

    sealed class TaskAwaiterProxy(TaskAwaiter taskAwaiter) : ITaskAwaiter
    {
        public bool IsCompleted => taskAwaiter.IsCompleted;

        public void GetResult() => taskAwaiter.GetResult();
        public void OnCompleted(Action continuation) => taskAwaiter.OnCompleted(continuation);
        public void UnsafeOnCompleted(Action continuation) => taskAwaiter.UnsafeOnCompleted(continuation);
    }
}
