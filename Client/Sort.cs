namespace Foundation;

public record Sort()
{
    public IEnumerable<TItem> Apply<TItem>(IEnumerable<TItem> collection)
        => collection;
}