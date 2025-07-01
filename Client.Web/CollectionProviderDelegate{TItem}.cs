namespace Foundation;

public delegate Task<TItem[]> CollectionProviderDelegate<TItem>(Transform transform, CancellationToken cancellationToken = default);
