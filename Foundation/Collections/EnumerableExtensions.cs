namespace Foundation.Collections;

public static class EnumerableExtensions
{
    public static void ForEach<TItem>(this IEnumerable<TItem> @this, Action<TItem> action)
    {
        foreach (var item in @this)
        {
            action(item);
        }
    }
}
