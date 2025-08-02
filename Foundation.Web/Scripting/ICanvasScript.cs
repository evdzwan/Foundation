using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripting;

public interface ICanvasScript
{
    ValueTask Cleanup<TValue>(DotNetObjectReference<TValue> component, CancellationToken cancellationToken = default) where TValue : class
        => ValueTask.CompletedTask;

    ValueTask Initialize<TValue>(DotNetObjectReference<TValue> component, ElementReference canvas, CancellationToken cancellationToken = default) where TValue : class
        => ValueTask.CompletedTask;

    ValueTask Resize<TValue>(DotNetObjectReference<TValue> component, ElementReference canvas, CancellationToken cancellationToken = default) where TValue : class
        => ValueTask.CompletedTask;
}
