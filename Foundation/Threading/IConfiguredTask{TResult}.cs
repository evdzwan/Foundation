namespace Foundation.Threading;

public interface IConfiguredTask<out TResult> : IConfiguredTask
{
    ITaskAwaiter IConfiguredTask.GetAwaiter() => GetAwaiter();
    new ITaskAwaiter<TResult> GetAwaiter();
}
