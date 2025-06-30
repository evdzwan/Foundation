using Microsoft.Extensions.DependencyInjection;

namespace Foundation;

sealed class FoundationConfiguration(IServiceCollection services) : IFoundationConfiguration
{
    public IFoundationConfiguration Scan(IEnumerable<Type> types)
    {
        types.ForEach(type =>
        {
            RegisterCommandHandlers(type);
            RegisterCommandObservers(type);
            RegisterMutators(type);
        });

        return this;
    }

    void RegisterCommandHandlers(Type type)
    {
        var interfaceTypes = type.GetInterfaces()
                                 .Where(t => t is { IsGenericType: true, IsGenericTypeDefinition: false } && t.GetGenericTypeDefinition().IsAssignableTo(typeof(ICommandHandler<>)))
                                 .ToArray();

        if (interfaceTypes.Length > 0)
        {
            services.AddScoped(type);
            interfaceTypes.ForEach(interfaceType => services.AddTransient(interfaceType, sp => sp.GetRequiredService(type)));
        }
    }

    void RegisterCommandObservers(Type type)
    {
        if (type is { IsAbstract: false, IsClass: true } && type.IsAssignableTo(typeof(ICommandObserver)))
        {
            services.AddScoped(type);
            services.AddTransient(typeof(ICommandObserver), sp => sp.GetRequiredService(type));
        }
    }

    void RegisterMutators(Type type)
    {
        var interfaceTypes = type.GetInterfaces()
                                 .Where(t => t is { IsGenericType: true, IsGenericTypeDefinition: false } && t.GetGenericTypeDefinition().IsAssignableTo(typeof(IMutator<,>)))
                                 .ToArray();

        if (interfaceTypes.Length > 0)
        {
            services.AddScoped(type);
            interfaceTypes.ForEach(interfaceType =>
            {
                services.AddTransient(interfaceType, sp => sp.GetRequiredService(type));
                services.AddTransient(typeof(IMutator<>).MakeGenericType(interfaceType.GenericTypeArguments[1]), sp => sp.GetRequiredService(type));
            });
        }
    }
}
