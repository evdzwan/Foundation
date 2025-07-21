namespace Foundation.Threading;

public interface ITaskAwaiter<out TResult> : ITaskAwaiter
{
    TResult GetResult();
}
