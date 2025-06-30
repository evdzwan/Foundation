namespace Foundation;

public interface IMutator<TModel, in TCommand> : IMutator<TCommand> where TModel : notnull
{
    Type IMutator<TCommand>.ModelType => typeof(TModel);

    TModel On(TModel model, TCommand command);
    object IMutator<TCommand>.On(object model, TCommand command)
        => On((TModel)model, command);
}
