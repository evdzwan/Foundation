using System.Numerics;

namespace Foundation.Collections;

public static class NumberExtensions
{
    public static INumber<TValue> Sum<TValue>(this IEnumerable<TValue> @this) where TValue : INumber<TValue>
    {
        var sum = TValue.Zero;
        @this.ForEach(value => sum += value);
        return sum;
    }
}
