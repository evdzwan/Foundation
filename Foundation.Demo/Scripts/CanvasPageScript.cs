using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script("./_content/Foundation.Demo/Components/Pages/CanvasPage.razor.js")]
sealed partial class CanvasPageScript
{
    public partial ValueTask Cleanup(ElementReference canvas, CancellationToken cancellationToken = default);
    public partial ValueTask Initialize<TValue>(ElementReference canvas, DotNetObjectReference<TValue> invoker, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask Resize(ElementReference canvas, CancellationToken cancellationToken = default);
}
