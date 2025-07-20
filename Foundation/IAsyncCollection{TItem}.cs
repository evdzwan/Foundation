namespace Foundation;

public interface IAsyncCollection<out TItem> : IAsyncEnumerable<TItem>, IAsyncValue<TItem[]>
{
}
