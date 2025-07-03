using Foundation.Scripting;
using Microsoft.AspNetCore.Components;

namespace Foundation.Scripts;

[Script(@"./_content/Client.Web/Components/Focus.razor.js")]
sealed partial class FocusScript
{
    public partial ValueTask Focus(ElementReference elem, string selector, CancellationToken cancellationToken = default);
}
