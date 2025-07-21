using System.Collections.ObjectModel;

namespace Foundation.Collections;

sealed class MultipleSelection<TItem>(IEnumerable<TItem> activeItems) : ObservableCollection<TItem>(activeItems), ISelection<TItem>
{
    public bool Multiple { get; } = true;

    public void Activate(TItem item)
        => Add(item);
}
