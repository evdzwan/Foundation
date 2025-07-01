namespace Foundation;

sealed class SingleSelection<TItem> : List<TItem>, ISelection<TItem>
{
    public TItem? Cursor => this.LastOrDefault();

    public bool Multiple { get; } = false;

    public void Activate(TItem item)
    {
        Clear();
        Add(item);
    }
}
