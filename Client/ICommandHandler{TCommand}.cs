namespace Foundation;

public interface ICommandHandler<in TCommand>
{
    Task On(TCommand command, ICommandDispatcher dispatcher, CancellationToken cancellationToken);
}
