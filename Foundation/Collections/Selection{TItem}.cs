namespace Foundation.Collections;

abstract class Selection<TItem>(IEnumerable<TItem> activeItems) : List<TItem>(activeItems), ISelection<TItem>
{
    readonly List<Action<ISelection<TItem>>> ChangedHandlers = [];
    readonly Lock UpdateHandlersLock = new();

    public abstract bool Multiple { get; }

    protected virtual void ActivateItem(TItem item) => Add(item);
    public void Activate(TItem item)
    {
        ActivateItem(item);
        NotifyChangedHandlers();
    }

    protected virtual bool DeactivateItem(TItem item) => Remove(item);
    public bool Deactivate(TItem item)
    {
        if (DeactivateItem(item))
        {
            NotifyChangedHandlers();
            return true;
        }

        return false;
    }

    public IDisposable Subscribe(Action<ISelection<TItem>> changedHandler)
    {
        using (UpdateHandlersLock.EnterScope())
        {
            ChangedHandlers.Add(changedHandler);
        }

        return new Disposable(() =>
        {
            using (UpdateHandlersLock.EnterScope())
            {
                ChangedHandlers.Remove(changedHandler);
            }
        });
    }

    public void Toggle(TItem item)
    {
        if (!DeactivateItem(item))
        {
            ActivateItem(item);
        }

        NotifyChangedHandlers();
    }

    void NotifyChangedHandlers()
    {
        using (UpdateHandlersLock.EnterScope())
        {
            ChangedHandlers.ForEach(handler => handler(this));
        }
    }
}
