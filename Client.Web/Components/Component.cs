using Microsoft.AspNetCore.Components;

namespace Foundation.Components;

public abstract class Component : ComponentBase, IAsyncDisposable
{
    protected CancellationTokenSource ComponentLife { get; } = new();

    protected virtual void OnDisposed() { }
    protected virtual ValueTask OnDisposedAsync() => ValueTask.CompletedTask;

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        OnDisposed();
        await OnDisposedAsync();

        await ComponentLife.CancelAsync();
        ComponentLife.Dispose();

        GC.SuppressFinalize(this);
    }
}
