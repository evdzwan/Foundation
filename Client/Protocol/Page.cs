namespace Foundation.Protocol;

public sealed record Page(int Skip = 0, int Take = 20)
{
    public IQueryable<TItem> Transform<TItem>(IQueryable<TItem> collection)
        => collection.Skip(Skip).Take(Take);
}
