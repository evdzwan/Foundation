namespace Foundation;

public interface IMutator<TModel, TCommand>
{
    TModel On(TModel model, TCommand command);
}
