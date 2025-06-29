using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Foundation;

sealed class ReflectionEffect<TAction>(MethodInfo method, IServiceProvider serviceProvider) : IEffect<TAction>
{
    public Task Apply(TAction action, IDispatcher dispatcher)
    {
        var declaringInstance = method is { IsStatic: false, DeclaringType: { } declaringType } ? serviceProvider.GetRequiredService(declaringType) : null;
        if (method.Invoke(declaringInstance, [action, dispatcher]) is Task task)
        {
            return task;
        }

        throw new InvalidOperationException($"Method {method.Name} must have return type {nameof(Task)}");
    }
}
