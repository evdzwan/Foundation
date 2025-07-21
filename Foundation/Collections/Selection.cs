namespace Foundation.Collections;

public static class Selection
{
    public static ISelection<TItem> Multiple<TItem>()
        => new MultipleSelection<TItem>(activeItems: []);

    public static ISelection<TItem> Multiple<TItem>(IEnumerable<TItem> activeItems)
        => new MultipleSelection<TItem>(activeItems);

    public static ISelection<TItem> Single<TItem>()
        => new SingleSelection<TItem>(activeItem: default);

    public static ISelection<TItem> Single<TItem>(TItem? activeItem)
        => new SingleSelection<TItem>(activeItem);
}
