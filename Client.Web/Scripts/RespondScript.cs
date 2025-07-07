using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script("./_content/Client.Web/Components/Respond.razor.js")]
sealed partial class RespondScript
{
    public partial ValueTask Attach<TValue>(ElementReference elem, DotNetObjectReference<TValue> invoker) where TValue : class;
    public partial ValueTask Detach(ElementReference elem);
}
