using Foundation.Scripting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Foundation.Scripts;

[Script(@"./_content/Foundation.Web/Components/Menu.razor.js")]
sealed partial class MenuScript
{
    public partial ValueTask Attach<TValue>(DotNetObjectReference<TValue> component, ElementReference element, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask Detach<TValue>(DotNetObjectReference<TValue> component, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask ShowMenu(ElementReference element, CancellationToken cancellationToken = default);
    public partial ValueTask HideMenu(ElementReference element, CancellationToken cancellationToken = default);
}
