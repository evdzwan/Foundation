using System.Collections.ObjectModel;

namespace Foundation;

public sealed class SingleSelection<TItem> : ObservableCollection<TItem>, ISelection<TItem>
{
    public TItem? Cursor => this.LastOrDefault();

    public bool Multiple { get; } = false;

    public void Activate(TItem item)
    {
        Clear();
        Add(item);
    }
}
