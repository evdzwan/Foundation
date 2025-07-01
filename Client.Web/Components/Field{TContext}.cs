using Microsoft.AspNetCore.Components;

namespace Foundation.Components;

public abstract class Field<TContext> : ComponentBase, IDisposable where TContext : class
{
    [CascadingParameter] protected TContext? Context { get; private set; }
    [CascadingParameter] protected Form<TContext>? Form { get; private init; }

    [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object?>? Attributes { get; set; }
    [Parameter] public bool AutoFocus { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string? Title { get; set; }
    [Parameter] public bool Visible { get; set; } = true;

    protected override void OnInitialized() => Form?.AddField(this);
    void IDisposable.Dispose()
    {
        GC.SuppressFinalize(this);
        Form?.RemoveField(this);
    }
}
