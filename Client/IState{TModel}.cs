namespace Foundation;

public interface IState<TModel>
{
    TModel Model { get;  }

    IDisposable Subscribe(Action<TModel> modelChanged);
}
