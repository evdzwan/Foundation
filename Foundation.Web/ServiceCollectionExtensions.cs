using Foundation.Collections;
using Foundation.Scripting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Foundation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoundation(this IServiceCollection @this)
        => @this.AddScripts([typeof(ServiceCollectionExtensions).Assembly])
                .AddScoped<MenuController>();

    public static IServiceCollection AddScripts(this IServiceCollection @this, IEnumerable<Assembly> assemblies)
    {
        assemblies.SelectMany(asm => asm.GetTypes())
                  .Where(type => type is { IsAbstract: false, IsClass: true, IsGenericType: false })
                  .Where(type => type.IsAssignableTo(typeof(Script)))
                  .ForEach(scriptType => @this.AddTransient(scriptType));

        return @this;
    }
}
