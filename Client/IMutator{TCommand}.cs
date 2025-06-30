namespace Foundation;

public interface IMutator<in TCommand>
{
    Type ModelType { get; }

    internal object On(object model, TCommand command);
}
