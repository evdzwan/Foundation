using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoundation(this IServiceCollection @this, Action<IFoundationConfiguration>? configure = null)
    {
        var config = new FoundationConfiguration(@this);
        configure?.Invoke(config);
        return @this;
    }
}
