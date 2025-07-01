namespace Foundation;

public static class Selection
{
    public static ISelection<TItem> Multiple<TItem>()
        => new MultipleSelection<TItem>();

    public static ISelection<TItem> Single<TItem>()
        => new SingleSelection<TItem>();
}
