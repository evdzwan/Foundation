using Microsoft.AspNetCore.Components;

namespace Foundation;

public abstract class Component : ComponentBase, IAsyncDisposable
{
    readonly CancellationTokenSource CancellationTokenSource = new();

    protected virtual void OnDisposing() { }
    protected virtual Task OnDisposingAsync() => Task.CompletedTask;
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        OnDisposing();
        await OnDisposingAsync();

        await CancellationTokenSource.CancelAsync();
        CancellationTokenSource.Dispose();
        GC.SuppressFinalize(this);
    }
}
