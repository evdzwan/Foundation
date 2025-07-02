namespace Foundation;

public record Transform(Page? Page)
{
    public IEnumerable<TItem> Apply<TItem>(IEnumerable<TItem> collection)
    {
        if (Page is { } page)
        {
            collection = collection.Skip(page.Skip).Take(page.Take);
        }

        return collection;
    }
}
