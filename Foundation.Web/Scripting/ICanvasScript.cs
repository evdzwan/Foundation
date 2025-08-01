using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripting;

public interface ICanvasScript
{
    ValueTask Cleanup(ElementReference canvas, CancellationToken cancellationToken = default);
    ValueTask Initialize<TValue>(ElementReference canvas, DotNetObjectReference<TValue> invoker, CancellationToken cancellationToken = default) where TValue : class;
    ValueTask Resize(ElementReference canvas, CancellationToken cancellationToken = default);
}
