using System.Collections.Specialized;

namespace Foundation.Collections;

public interface ISelection<TItem> : IList<TItem>, INotifyCollectionChanged
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
