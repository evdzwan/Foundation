using Foundation.Scripting;
using Microsoft.AspNetCore.Components;

namespace Foundation.Scripts;

[Script("./balls.js")]
sealed partial class BallsScript : ICanvasScript
{
    public partial ValueTask Cleanup(object state, CancellationToken cancellationToken = default);
    public partial ValueTask Initialize(ElementReference canvas, object state, CancellationToken cancellationToken = default);
}
