namespace Foundation.Protocol;

public sealed record Page(int Skip, int Take)
{
    public IQueryable<TItem> Transform<TItem>(IQueryable<TItem> collection)
        => collection.Skip(Skip).Take(Take);
}
