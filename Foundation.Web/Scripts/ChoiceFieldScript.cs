using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script(@"./_content/Foundation.Web/Components/ChoiceField.razor.js")]
sealed partial class ChoiceFieldScript
{
    public partial ValueTask Attach<TValue>(ElementReference elem, DotNetObjectReference<TValue> invoker, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask Detach(ElementReference elem, CancellationToken cancellationToken = default);
    public partial ValueTask ShowPopover(ElementReference elem, ElementReference trigger, CancellationToken cancellationToken = default);
    public partial ValueTask HidePopover(ElementReference elem, CancellationToken cancellationToken = default);
}
