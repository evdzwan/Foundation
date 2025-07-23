namespace Foundation.Collections;

public interface ISelection<TItem> : IList<TItem>
{
    TItem? Cursor => this.LastOrDefault();
    bool Multiple { get; }

    void Activate(TItem item);
    bool Deactivate(TItem item);
    bool IsActive(TItem item) => Contains(item);
    void Toggle(TItem item);

    IDisposable Subscribe(Action<ISelection<TItem>> handler);
}
