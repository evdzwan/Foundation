namespace Foundation;

public interface IState<TModel> : IState where TModel : notnull
{
    new TModel Model { get; }
    object IState.Model => Model;
    Type IState.ModelType => typeof(TModel);

    IDisposable Subscribe(Action<TModel> modelChanged);
}
