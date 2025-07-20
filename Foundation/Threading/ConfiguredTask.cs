using System.Runtime.CompilerServices;

namespace Foundation.Threading;

sealed class ConfiguredTask(Task task, bool continueOnCapturedContext) : IConfiguredTask
{
    ConfiguredTaskAwaitable Task { get; } = task.ConfigureAwait(continueOnCapturedContext);

    public ITaskAwaiter GetAwaiter() => new ConfiguredTaskAwaiter(Task.GetAwaiter());

    sealed class ConfiguredTaskAwaiter(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter taskAwaiter) : ITaskAwaiter
    {
        public bool IsCompleted => taskAwaiter.IsCompleted;

        public void GetResult() => taskAwaiter.GetResult();
        public void OnCompleted(Action continuation) => taskAwaiter.OnCompleted(continuation);
        public void UnsafeOnCompleted(Action continuation) => taskAwaiter.UnsafeOnCompleted(continuation);
    }
}
