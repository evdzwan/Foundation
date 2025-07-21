using System.Collections.ObjectModel;

namespace Foundation.Collections;

sealed class SingleSelection<TItem>(TItem? activeItem) : ObservableCollection<TItem>(activeItem is { } ? [activeItem] : []), ISelection<TItem>
{
    public bool Multiple { get; } = false;

    public void Activate(TItem item)
    {
        if (this is ISelection<TItem> { Cursor: { } cursor })
        {
            Remove(cursor);
        }

        Add(item);
    }
}
