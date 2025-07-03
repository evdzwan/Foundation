using System.Collections.Specialized;

namespace Foundation;

public interface ISelection<TItem> : ICollection<TItem>, INotifyCollectionChanged where TItem : notnull
{
    TItem? Cursor => this.LastOrDefault();
    bool Multiple { get; }

    void Activate(TItem item);
    bool Deactivate(TItem item) => Remove(item);
    bool IsActive(TItem item) => Contains(item);
    void Toggle(TItem item)
    {
        if (!Deactivate(item))
        {
            Activate(item);
        }
    }
}
