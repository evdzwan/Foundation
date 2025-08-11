using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script(@"./_content/EvdZwan.Foundation.Web/Components/Dialog.razor.js")]
sealed partial class DialogScript
{
    public partial ValueTask Attach<TValue>(DotNetObjectReference<TValue> component, ElementReference dialog, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask Detach<TValue>(DotNetObjectReference<TValue> component, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask HideDialog(ElementReference dialog, CancellationToken cancellationToken = default);
    public partial ValueTask ShowDialog(ElementReference dialog, ElementReference? source = null, CancellationToken cancellationToken = default);
}
