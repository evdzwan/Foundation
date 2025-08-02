using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script(@"./_content/Foundation.Web/Components/ChoiceField.razor.js")]
sealed partial class ChoiceFieldScript
{
    public partial ValueTask Attach<TValue>(DotNetObjectReference<TValue> component, ElementReference element, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask Detach<TValue>(DotNetObjectReference<TValue> component, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask ShowPopover(ElementReference element, ElementReference trigger, CancellationToken cancellationToken = default);
    public partial ValueTask HidePopover(ElementReference element, CancellationToken cancellationToken = default);
}
