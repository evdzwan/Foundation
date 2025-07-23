namespace Foundation.Collections;

abstract class Selection<TItem>(IEnumerable<TItem> activeItems) : List<TItem>(activeItems), ISelection<TItem>
{
    readonly List<Action<ISelection<TItem>>> Handlers = [];
    readonly Lock UpdateHandlersLock = new();

    public abstract bool Multiple { get; }

    protected virtual void ActivateItem(TItem item) => Add(item);
    public void Activate(TItem item)
    {
        ActivateItem(item);
        NotifyHandlers();
    }

    protected virtual bool DeactivateItem(TItem item) => Remove(item);
    public bool Deactivate(TItem item)
    {
        if (DeactivateItem(item))
        {
            NotifyHandlers();
            return true;
        }

        return false;
    }

    public IDisposable Subscribe(Action<ISelection<TItem>> handler)
    {
        using (UpdateHandlersLock.EnterScope())
        {
            Handlers.Add(handler);
        }

        return new Disposable(() =>
        {
            using (UpdateHandlersLock.EnterScope())
            {
                Handlers.Remove(handler);
            }
        });
    }

    public void Toggle(TItem item)
    {
        if (!DeactivateItem(item))
        {
            ActivateItem(item);
        }

        NotifyHandlers();
    }

    void NotifyHandlers()
    {
        using (UpdateHandlersLock.EnterScope())
        {
            Handlers.ForEach(handler => handler(this));
        }
    }
}
