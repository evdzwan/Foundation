namespace Foundation;

public interface IInvoker
{
    Func<CancellationToken, Task>? Invoke { get; set; }
}
