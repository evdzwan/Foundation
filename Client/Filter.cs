namespace Foundation;

public record Filter()
{
    public IEnumerable<TItem> Apply<TItem>(IEnumerable<TItem> collection)
        => collection;
}
