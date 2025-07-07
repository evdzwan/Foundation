namespace Foundation;

public interface IInvoke
{
    void SetHandler(Func<CancellationToken, Task>? handler);
}
