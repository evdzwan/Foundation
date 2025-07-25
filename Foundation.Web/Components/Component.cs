using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Components;

public abstract class Component : ComponentBase, IAsyncDisposable
{
    readonly CancellationTokenSource CancellationTokenSource = new();
    readonly List<IDisposable> SurfaceSubscriptions = [];
    DotNetObjectReference<Component>? Reference;

    [Inject] SurfaceController? Surface { get; init; }

    protected CancellationToken CancellationToken => CancellationTokenSource.Token;
    protected DotNetObjectReference<Component> ComponentReference => Reference ??= DotNetObjectReference.Create(this);

    protected IDisposable? AddToSurface(RenderFragment fragment)
    {
        if (Surface?.AddFragment(fragment) is { } subscription)
        {
            SurfaceSubscriptions.Add(subscription);
            return subscription;
        }

        return null;
    }

    protected virtual void OnDisposing() { }
    protected virtual Task OnDisposingAsync() => Task.CompletedTask;
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        OnDisposing();
        await OnDisposingAsync();

        SurfaceSubscriptions.ForEach(subscription => subscription.Dispose());
        SurfaceSubscriptions.Clear();

        await CancellationTokenSource.CancelAsync();
        CancellationTokenSource.Dispose();
        GC.SuppressFinalize(this);
    }
}
