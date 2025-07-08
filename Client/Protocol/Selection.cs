using System.Collections.ObjectModel;

namespace Foundation.Protocol;

public static class Selection
{
    public static ISelection<TItem> Multiple<TItem>() where TItem : notnull
        => new MultipleSelection<TItem>([]);

    public static ISelection<TItem> Multiple<TItem>(IEnumerable<TItem> collection) where TItem : notnull
        => new MultipleSelection<TItem>(collection);

    sealed class MultipleSelection<TItem>(IEnumerable<TItem> collection) : ObservableCollection<TItem>(collection), ISelection<TItem> where TItem : notnull
    {
        public bool Multiple { get; } = true;

        public void Activate(TItem item)
        {
            using var _ = BlockReentrancy();
            if (!Contains(item))
            {
                Add(item);
            }
        }
    }

    public static ISelection<TItem> Single<TItem>() where TItem : notnull
        => new SingleSelection<TItem>([]);

    public static ISelection<TItem> Single<TItem>(IEnumerable<TItem> collection) where TItem : notnull
        => new SingleSelection<TItem>(collection);

    sealed class SingleSelection<TItem>(IEnumerable<TItem> collection) : ObservableCollection<TItem>(collection), ISelection<TItem> where TItem : notnull
    {
        public bool Multiple { get; } = false;

        public void Activate(TItem item)
        {
            using var _ = BlockReentrancy();
            if (this.LastOrDefault() is { } cursor)
            {
                Remove(cursor);
            }

            Add(item);
        }
    }
}
