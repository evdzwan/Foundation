namespace Foundation;

public delegate Task<TValue> ValueProviderDelegate<TValue>(CancellationToken cancellationToken = default);
