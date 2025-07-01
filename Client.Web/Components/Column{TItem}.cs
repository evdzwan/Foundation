using Microsoft.AspNetCore.Components;

namespace Foundation.Components;

public abstract class Column<TItem> : ComponentBase, IDisposable
{
    [CascadingParameter] protected Table<TItem>? Table { get; private init; }
    [Parameter] public string? Title { get; set; }

    public abstract RenderFragment RenderCell(TItem item);

    protected override void OnInitialized() => Table?.AddColumn(this);
    void IDisposable.Dispose()
    {
        GC.SuppressFinalize(this);
        Table?.RemoveColumn(this);
    }
}
