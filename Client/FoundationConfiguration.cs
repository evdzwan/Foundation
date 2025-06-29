using Microsoft.Extensions.DependencyInjection;

namespace Foundation;

sealed class FoundationConfiguration(IServiceCollection services) : IFoundationConfiguration
{
    public IFoundationConfiguration AddMiddleware(Type middlewareType)
    {
        if (middlewareType is { IsAbstract: false, IsClass: true } && middlewareType.IsAssignableTo(typeof(IMiddleware)))
        {
            services.AddScoped(typeof(IMiddleware), middlewareType);
            return this;
        }

        throw new ArgumentException($"Type must be a concrete class that implements {nameof(IMiddleware)}", nameof(middlewareType));
    }

    public IFoundationConfiguration ScanTypes(IEnumerable<Type> types)
    {
        foreach (var type in types)
        {
            if (GetEffectDataOrDefault(type) is { Length: > 0 } effectDataCollection)
            {
                foreach (var effectData in effectDataCollection)
                {
                    var interfaceType = typeof(IEffect<>).MakeGenericType([effectData.ActionType]);
                    services.AddScoped(interfaceType, sp => Activator.CreateInstance(typeof(ReflectionEffect<>).MakeGenericType([[effectData.ActionType]]), [type, effectData.Method]));
                    if (!type.IsAbstract)
                    {
                        services.AddScoped(type);
                    }
                }
            }
            else if (GetFeatureDataOrDefault(type) is { Length: > 0 } featureDataCollection)
            {
            }
            else if (GetReducerDataOrDefault(type) is { Length: > 0 } reducerDataCollection)
            {
            }
        }

        return this;
    }
}
