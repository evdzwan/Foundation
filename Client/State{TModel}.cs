namespace Foundation;

sealed class State<TModel> : IState<TModel>, IDisposable where TModel : notnull
{
    readonly List<StateSubscription> Subscriptions = [];

    public TModel Model { get; private set; } = CreateDefaultModel();

    public IDisposable Subscribe(Action<TModel> modelChanged)
    {
        var subscription = new StateSubscription(Subscriptions, modelChanged);
        Subscriptions.Add(subscription);
        return subscription;
    }

    void SetModel(TModel model)
    {
        if (!ReferenceEquals(model, Model))
        {
            Model = model;
            Subscriptions.ForEach(subscription => subscription.Invoke(model));
        }
    }

    void IState.SetModel(object model)
        => SetModel((TModel)model);

    void IDisposable.Dispose()
    {
        Subscriptions.ToArray().ForEach(subscription => subscription.Dispose());
        Subscriptions.Clear();
    }

    static TModel CreateDefaultModel() => typeof(TModel) switch
    {
        _ when typeof(TModel).IsAssignableTo(typeof(ICreateNew<>)
                             .MakeGenericType([typeof(TModel)])) => (TModel)typeof(TModel).GetMethod(nameof(ICreateNew<TModel>.CreateNew))!
                                                                                          .Invoke(obj: null, parameters: [])!,
        _ => Activator.CreateInstance<TModel>()
    };

    class StateSubscription(ICollection<StateSubscription> subscriptions, Action<TModel> modelChanged) : IDisposable
    {
        public void Invoke(TModel model)
            => modelChanged(model);

        public void Dispose()
        {
            subscriptions.Remove(this);
            modelChanged = null!;
        }
    }
}
