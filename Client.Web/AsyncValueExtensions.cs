namespace Foundation;

public static class AsyncValueExtensions
{
    public static ValueProviderDelegate<TValue> AsValueProvider<TValue>(this IAsyncValue<TValue> @this)
        => new(@this.GetValue);
}
