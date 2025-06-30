namespace Foundation;

public interface IMutator<TModel, TCommand> : IMutator<TCommand>
{
    Type IMutator<TCommand>.ModelType => typeof(TModel);

    TModel On(TModel model, TCommand command);
    object IMutator<TCommand>.On(object model, TCommand command)
    {
        var onMethod = typeof(IMutator<TModel, TCommand>).GetMethod(nameof(On), genericParameterCount: 0, [typeof(TModel), typeof(TCommand)])!;
        return onMethod.Invoke(this, [model, command])!;
    }
}
