using System.Collections.ObjectModel;

namespace Foundation;

public sealed class MultipleSelection<TItem> : ObservableCollection<TItem>, ISelection<TItem>
{
    public TItem? Cursor => this.LastOrDefault();

    public bool Multiple { get; } = true;

    public void Activate(TItem item)
        => Add(item);
}
