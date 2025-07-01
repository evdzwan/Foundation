namespace Foundation;

sealed class MultipleSelection<TItem> : List<TItem>, ISelection<TItem>
{
    public TItem? Cursor => this.LastOrDefault();

    public bool Multiple { get; } = true;

    public void Activate(TItem item)
        => Add(item);
}
