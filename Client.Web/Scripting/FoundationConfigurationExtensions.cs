using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Foundation.Scripting;

public static class FoundationConfigurationExtensions
{
    public static IFoundationConfiguration AddScripts(this IFoundationConfiguration @this, IEnumerable<Assembly>? assemblies = null)
    {
        (assemblies ?? []).Prepend(typeof(FoundationConfigurationExtensions).Assembly)
                          .SelectMany(asm => asm.GetTypes())
                          .Where(type => !type.IsAbstract && type.IsAssignableTo(typeof(Script)))
                          .ForEach(scriptType => @this.Services.AddTransient(scriptType));

        return @this;
    }
}
