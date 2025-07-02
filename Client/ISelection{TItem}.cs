using System.Collections.Specialized;

namespace Foundation;

public interface ISelection<TItem> : ICollection<TItem>, INotifyCollectionChanged
{
    TItem? Cursor { get; }

    bool Multiple { get; }

    void Activate(TItem item);

    bool Deactivate(TItem item)
        => Remove(item);

    bool IsActive(TItem item)
        => Contains(item);

    void Toggle(TItem item)
    {
        if (!Deactivate(item))
        {
            Activate(item);
        }
    }
}
