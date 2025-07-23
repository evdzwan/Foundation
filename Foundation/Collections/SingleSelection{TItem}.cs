namespace Foundation.Collections;

sealed class SingleSelection<TItem>(TItem? activeItem) : Selection<TItem>(activeItem is { } ? [activeItem] : [])
{
    public override bool Multiple { get; } = false;

    protected override void ActivateItem(TItem item)
    {
        if (this is ISelection<TItem> { Cursor: { } cursor })
        {
            DeactivateItem(cursor);
        }

        base.ActivateItem(item);
    }
}
