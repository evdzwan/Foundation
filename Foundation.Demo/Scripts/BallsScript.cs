using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script("./balls.js")]
sealed partial class BallsScript : ICanvasScript
{
    public partial ValueTask Initialize<TValue>(DotNetObjectReference<TValue> component, ElementReference canvas, CancellationToken cancellationToken = default) where TValue : class;
}
