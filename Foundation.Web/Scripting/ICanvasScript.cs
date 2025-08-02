using Microsoft.AspNetCore.Components;

namespace Foundation.Scripting;

public interface ICanvasScript
{
    ValueTask Cleanup(object state, CancellationToken cancellationToken = default)
        => ValueTask.CompletedTask;

    ValueTask Initialize(ElementReference canvas, object state, CancellationToken cancellationToken = default)
        => ValueTask.CompletedTask;

    ValueTask Resize(ElementReference canvas, object state, CancellationToken cancellationToken = default)
        => ValueTask.CompletedTask;
}
