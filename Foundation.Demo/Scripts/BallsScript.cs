using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script("./balls.js")]
sealed partial class BallsScript : ICanvasScript
{
    public partial ValueTask Cleanup(ElementReference canvas, CancellationToken cancellationToken = default);
    public partial ValueTask Initialize<TValue>(ElementReference canvas, DotNetObjectReference<TValue> invoker, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask Resize(ElementReference canvas, CancellationToken cancellationToken = default);
}
