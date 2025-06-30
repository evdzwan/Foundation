namespace Foundation;

public interface ICommandHandler<TCommand>
{
    Task On(TCommand command, ICommandDispatcher dispatcher, CancellationToken cancellationToken);
}
