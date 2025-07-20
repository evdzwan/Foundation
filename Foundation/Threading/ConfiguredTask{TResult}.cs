using System.Runtime.CompilerServices;

namespace Foundation.Threading;

sealed class ConfiguredTask<TResult>(Task<TResult> task, bool continueOnCapturedContext) : IConfiguredTask<TResult>
{
    ConfiguredTaskAwaitable<TResult> Task { get; } = task.ConfigureAwait(continueOnCapturedContext);

    public ITaskAwaiter<TResult> GetAwaiter()
        => new ConfiguredTaskAwaiter(Task.GetAwaiter());

    sealed class ConfiguredTaskAwaiter(ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter taskAwaiter) : ITaskAwaiter<TResult>
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
