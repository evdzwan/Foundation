namespace Foundation.Collections;

sealed class MultipleSelection<TItem>(IEnumerable<TItem> activeItems) : Selection<TItem>(activeItems)
{
    public override bool Multiple { get; } = true;
}
