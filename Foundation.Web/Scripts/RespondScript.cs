using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script("./_content/EvdZwan.Foundation.Web/Components/Respond.razor.js")]
sealed partial class RespondScript
{
    public partial ValueTask Attach<TValue>(DotNetObjectReference<TValue> component, ElementReference element, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask Detach<TValue>(DotNetObjectReference<TValue> component, CancellationToken cancellationToken = default) where TValue : class;
}
