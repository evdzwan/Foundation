using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

sealed class ChoiceFieldScript(IJSRuntime jsRuntime, ILogger<ChoiceFieldScript>? logger = null) : Script(jsRuntime, "./_content/Client.Web/Components/ChoiceField.razor.js", logger)
{
    public ValueTask Attach<TValue>(ElementReference elem, DotNetObjectReference<TValue> invoker, CancellationToken cancellationToken = default) where TValue : class
        => Invoke("attach", [elem, invoker], cancellationToken);

    public ValueTask Detach(ElementReference elem, CancellationToken cancellationToken = default)
        => Invoke("detach", [elem], cancellationToken);

    public ValueTask ShowPopover(ElementReference elem, ElementReference trigger, CancellationToken cancellationToken = default)
        => Invoke("showPopover", [elem, trigger], cancellationToken);

    public ValueTask HidePopover(ElementReference elem, CancellationToken cancellationToken = default)
        => Invoke("hidePopover", [elem], cancellationToken);
}
