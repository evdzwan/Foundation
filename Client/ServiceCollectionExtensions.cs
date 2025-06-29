using Microsoft.Extensions.DependencyInjection;

namespace Foundation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoundation(this IServiceCollection @this, Action<IFoundationConfiguration>? config = null)
    {
        var configuration = new FoundationConfiguration(@this);
        config?.Invoke(configuration);
        return @this;
    }
}
