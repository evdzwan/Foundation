using Foundation.Collections;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Foundation.Scripting;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScripts(this IServiceCollection @this, IEnumerable<Assembly>? assemblies = null)
    {
        (assemblies ?? []).Prepend(typeof(ServiceCollectionExtensions).Assembly)
                          .SelectMany(asm => asm.GetTypes())
                          .Where(type => type is { IsAbstract: false, IsClass: true, IsGenericType: false })
                          .Where(type => type.IsAssignableTo(typeof(Script)))
                          .ForEach(scriptType => @this.AddTransient(scriptType));

        return @this;
    }
}
