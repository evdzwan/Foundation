namespace Foundation.Protocol;

public sealed record Query(Page? Page = null)
{
    public IQueryable<TItem> Transform<TItem>(IQueryable<TItem> collection)
        => Page?.Transform(collection) ?? collection;
}
