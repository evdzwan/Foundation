namespace Foundation.Threading;

public interface ITask<out TResult> : ITask
{
    TResult Result { get; }
}
