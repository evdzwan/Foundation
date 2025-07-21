using System.Runtime.CompilerServices;

namespace Foundation.Threading;

sealed class TaskProxy<TResult>(Task<TResult> task) : ITask<TResult>
{
    public TResult Result => task.Result;

    public IConfiguredTask<TResult> ConfigureAwait(bool continueOnCapturedContext)
        => new ConfiguredTask<TResult>(task, continueOnCapturedContext);

    public ITaskAwaiter<TResult> GetAwaiter()
        => new TaskAwaiterProxy(task.GetAwaiter());

    sealed class TaskAwaiterProxy(TaskAwaiter<TResult> taskAwaiter) : ITaskAwaiter<TResult>
    {
        public bool IsCompleted => taskAwaiter.IsCompleted;

        public TResult GetResult()
            => taskAwaiter.GetResult();

        public void OnCompleted(Action continuation)
            => taskAwaiter.OnCompleted(continuation);

        public void UnsafeOnCompleted(Action continuation)
            => taskAwaiter.UnsafeOnCompleted(continuation);
    }
}
