namespace Foundation;

public record Transform(Filter? Filter, Sort? Sort, Page? Page)
{
    public IEnumerable<TItem> Apply<TItem>(IEnumerable<TItem> collection)
    {
        collection = Filter?.Apply(collection) ?? collection;
        collection = Sort?.Apply(collection) ?? collection;
        collection = Page?.Apply(collection) ?? collection;
        return collection;
    }
}
