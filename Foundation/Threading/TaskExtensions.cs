namespace Foundation.Threading;

public static class TaskExtensions
{
    public static ITask AsITask(this Task @this) => new TaskProxy(@this);
    public static ITask<TResult> AsITask<TResult>(this Task<TResult> @this) => new TaskProxy<TResult>(@this);
}
