using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Foundation;

sealed class ReflectionReducer<TState, TAction>(MethodInfo method, IServiceProvider serviceProvider) : IReducer<TState, TAction>
{
    public TState Apply(TState state, TAction action)
    {
        var declaringInstance = method is { IsStatic: false, DeclaringType: { } declaringType } ? serviceProvider.GetRequiredService(declaringType) : null;
        if (method.Invoke(declaringInstance, [state, action]) is TState newState)
        {
            return newState;
        }

        throw new InvalidOperationException($"Method {method.Name} must have return type {typeof(TState).Name}");
    }
}
