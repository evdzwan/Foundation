namespace Foundation.Threading;

public interface IConfiguredTask
{
    ITaskAwaiter GetAwaiter();
}
