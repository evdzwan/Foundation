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

    public static void ForEach<TItem>(this IEnumerable<TItem> @this, Action<TItem, int> action)
    {
        foreach (var (item, index) in @this.Select((item, index) => (item, index)))
        {
            action(item, index);
        }
    }
}
