using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Foundation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoundation(this IServiceCollection @this)
        => @this.AddScripts(typeof(ServiceCollectionExtensions).Assembly);

    static IServiceCollection AddScripts(this IServiceCollection @this, Assembly assembly)
    {
        foreach (var scriptType in assembly.GetTypes().Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(Script))))
        {
            @this.AddScoped(scriptType);
        }

        return @this;
    }
}
