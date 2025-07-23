using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Components;

public abstract class Component : ComponentBase, IAsyncDisposable
{
    readonly CancellationTokenSource CancellationTokenSource = new();
    DotNetObjectReference<Component>? Reference;

    protected CancellationToken CancellationToken => CancellationTokenSource.Token;
    protected DotNetObjectReference<Component> ComponentReference => Reference ??= DotNetObjectReference.Create(this);

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
