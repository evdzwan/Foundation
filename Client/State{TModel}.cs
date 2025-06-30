namespace Foundation;

sealed class State<TModel> : IState<TModel>, IDisposable where TModel : notnull
{
    readonly List<ModelChangedSubscription> ModelChangedSubscriptions = [];

    public TModel Model { get; private set; } = CreateDefaultModel();
    object IState.Model => Model;

    public IDisposable Subscribe(Action<TModel> modelChanged)
    {
        var subscription = new ModelChangedSubscription(modelChanged, ModelChangedSubscriptions);
        ModelChangedSubscriptions.Add(subscription);
        return subscription;
    }

    void SetModel(TModel model)
    {
        if (!ReferenceEquals(model, Model))
        {
            Model = model;
            ModelChangedSubscriptions.ForEach(subscription => subscription.Invoke(model));
        }
    }

    void IState.SetModel(object model)
        => SetModel((TModel)model);

    void IDisposable.Dispose()
       => ModelChangedSubscriptions.ForEach(subscription => subscription.Dispose());

    static TModel CreateDefaultModel() => typeof(TModel) switch
    {
        { } when typeof(TModel).IsAssignableTo(typeof(ICreateNew<>)
                               .MakeGenericType([typeof(TModel)])) => (TModel)typeof(TModel).GetMethod(nameof(ICreateNew<TModel>.CreateNew))!
                                                                                            .Invoke(obj: null, parameters: [])!,
        _ => Activator.CreateInstance<TModel>()
    };

    class ModelChangedSubscription(Action<TModel> modelChanged, ICollection<ModelChangedSubscription> subscriptions) : IDisposable
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
