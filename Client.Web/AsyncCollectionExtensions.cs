namespace Foundation;

public static class AsyncCollectionExtensions
{
    public static CollectionProviderDelegate<TItem> AsCollectionProvider<TItem>(this IAsyncCollection<TItem> @this)
        => new(@this.GetView);
}
