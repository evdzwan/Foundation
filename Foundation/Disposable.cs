namespace Foundation;

public sealed class Disposable(Action action) : IDisposable
{
    public static Disposable Empty { get; } = new(() => { });

    void IDisposable.Dispose()
        => action();
}
