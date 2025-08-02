using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script("./_content/Foundation.Web/Components/Canvas.razor.js")]
sealed partial class CanvasScript
{
    public partial ValueTask Attach<TValue>(DotNetObjectReference<TValue> component, ElementReference element, IJSObjectReference? script, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask Detach<TValue>(DotNetObjectReference<TValue> component, CancellationToken cancellationToken = default) where TValue : class;
}
