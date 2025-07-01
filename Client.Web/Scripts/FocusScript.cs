using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

sealed class FocusScript(IJSRuntime jsRuntime, ILogger<FocusScript>? logger = null) : Script(jsRuntime, "./_content/Client.Web/Components/Focus.razor.js", logger)
{
    public ValueTask Focus(ElementReference elem, string selector, CancellationToken cancellationToken = default)
        => Invoke("focus", [elem, selector], cancellationToken);
}
