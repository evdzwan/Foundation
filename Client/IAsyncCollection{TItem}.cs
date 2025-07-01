namespace Foundation;

public interface IAsyncCollection<TItem> : IAsyncEnumerable<TItem>
{
    Task<TItem[]> GetView(Transform transform, CancellationToken cancellationToken = default);
}
