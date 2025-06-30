using Microsoft.Extensions.DependencyInjection;

namespace Foundation;

sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    void ICommandDispatcher.Dispatch(object command)
    {
        var dispatchMethod = GetType().GetMethod(nameof(Dispatch), genericParameterCount: 1, [Type.MakeGenericMethodParameter(0)])!;
        dispatchMethod.MakeGenericMethod([command.GetType()]).Invoke(this, [command]);
    }

    public void Dispatch<TCommand>(TCommand command) where TCommand : notnull
    {
        var observers = serviceProvider.GetServices<ICommandObserver>();
        if (!observers.Any(observer => !observer.CanInvokeCommand(command)))
        {
            var dispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
            var handlers = serviceProvider.GetServices<ICommandHandler<TCommand>>();
            var mutators = serviceProvider.GetServices<IMutator<TCommand>>();

            observers.ForEach(observer => observer.BeforeInvokeCommand(command));
            mutators.ForEach(mutator =>
            {
                var state = (IState)serviceProvider.GetRequiredService(typeof(IState<>).MakeGenericType(mutator.ModelType));
                state.SetModel(mutator.On(state.Model, command));
            });

            Task.WhenAll(handlers.Select(handler => handler.On(command, dispatcher, CancellationToken.None)));
            observers.ForEach(observer => observer.AfterInvokeCommand(command));
        }
    }
}
