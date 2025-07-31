using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script("./_content/Foundation.Web/Components/Canvas.razor.js")]
sealed partial class CanvasScript
{
    public partial ValueTask Attach(ElementReference elem, IJSObjectReference? script, CancellationToken cancellationToken = default);
    public partial ValueTask Detach(ElementReference elem, CancellationToken cancellationToken = default);
}
