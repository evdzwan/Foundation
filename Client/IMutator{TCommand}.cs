namespace Foundation;

public interface IMutator<TCommand>
{
    internal Type ModelType { get; }

    internal object On(object model, TCommand command);
}
