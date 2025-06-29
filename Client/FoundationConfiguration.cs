using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

    public IFoundationConfiguration Scan(IEnumerable<Type> types)
    {
        foreach (var type in types)
        {
            ScanEffects(type);
            ScanReducers(type);
        }

        return this;
    }

    void ScanEffects(Type type)
    {
        var registeredInstanceEffect = false;
        foreach (var method in type.GetMethods())
        {
            if (method.GetCustomAttribute<EffectAttribute>() is not null && method.GetParameters() is { Length: 2 } parameters)
            {
                var actionType = parameters[0].ParameterType;
                var interfaceType = typeof(IEffect<>).MakeGenericType([actionType]);
                var instanceType = typeof(ReflectionEffect<>).MakeGenericType([actionType]);

                services.AddScoped(interfaceType, sp => ActivatorUtilities.CreateInstance(sp, instanceType, [method, sp]));
                registeredInstanceEffect |= !method.IsStatic;
            }
        }

        if (registeredInstanceEffect)
        {
            services.AddTransient(type);
        }
    }

    void ScanReducers(Type type)
    {
        var registeredInstanceReducer = false;
        foreach (var method in type.GetMethods())
        {
            if (method.GetCustomAttribute<ReducerAttribute>() is not null && method.GetParameters() is { Length: 2 } parameters)
            {
                var stateType = parameters[0].ParameterType;
                var actionType = parameters[1].ParameterType;
                var interfaceType = typeof(IReducer<,>).MakeGenericType([stateType, actionType]);
                var instanceType = typeof(ReflectionReducer<,>).MakeGenericType([stateType, actionType]);

                services.AddScoped(interfaceType, sp => ActivatorUtilities.CreateInstance(sp, instanceType, [method, sp]));
                registeredInstanceReducer |= !method.IsStatic;
            }
        }

        if (registeredInstanceReducer)
        {
            services.AddTransient(type);
        }
    }
}
