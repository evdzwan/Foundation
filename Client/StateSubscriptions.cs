namespace Foundation;

public sealed class StateSubscriptions : IDisposable
{
    readonly List<IDisposable> Subscriptions = [];

    public IDisposable Add<TModel>(IState<TModel> state, Action<TModel> modelChanged, bool emitImmediately = true)
    {
        var subscription = state.Subscribe(modelChanged);
        Subscriptions.Add(subscription);
        if (emitImmediately)
        {
            modelChanged(state.Model);
        }

        return subscription;
    }

    public IDisposable AddThrottled<TModel>(IState<TModel> state, Action<TModel> modelChanged, TimeSpan throttleDelay, bool emitImmediately = true)
    {
        var gate = new Lock();
        var latest = (TModel?)default;
        var hasValue = false;
        var running = false;

        return Add(state, Handler, emitImmediately);

        void Handler(TModel model)
        {
            lock (gate)
            {
                latest = model;
                hasValue = true;

                if (running)
                {
                    return;
                }

                running = true;
            }

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(throttleDelay);

                    var value = (TModel?)default;
                    lock (gate)
                    {
                        if (!hasValue)
                        {
                            running = false;
                            return;
                        }

                        value = latest;
                        hasValue = false;
                    }

                    if (value is { })
                    {
                        modelChanged(value);
                    }
                }
            });
        }
    }

    public IDisposable AddDebounced<TModel>(IState<TModel> state, Action<TModel> modelChanged, TimeSpan debounceDelay, bool emitImmediately = true)
    {
        var gate = new Lock();
        var cts = new CancellationTokenSource();

        return Add(state, Handler, emitImmediately);

        void Handler(TModel model)
        {
            lock (gate)
            {
                cts.Cancel();
                cts = new();
            }

            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(debounceDelay, cts.Token);
                    if (!cts.Token.IsCancellationRequested)
                    {
                        modelChanged(model);
                    }
                }
                catch (TaskCanceledException) { }
            });
        }
    }

    public IDisposable AddSampled<TModel>(IState<TModel> state, Action<TModel> modelChanged, TimeSpan sampleInterval, bool emitImmediately = true)
    {
        var gate = new Lock();
        var nextAllowedTime = DateTime.MinValue;
        var latest = (TModel?)default;
        var hasValue = false;

        var timer = new Timer(_ =>
        {
            lock (gate)
            {
                if (hasValue)
                {
                    if (latest is { })
                    {
                        modelChanged(latest);
                    }

                    nextAllowedTime = DateTime.UtcNow + sampleInterval;
                    hasValue = false;
                }
            }
        }, state: null, dueTime: sampleInterval, period: sampleInterval);

        var subscription = new CompositeDisposable([state.Subscribe(Handler), timer]);
        Subscriptions.Add(subscription);
        if (emitImmediately)
        {
            modelChanged(state.Model);
        }

        return subscription;

        void Handler(TModel model)
        {
            lock (gate)
            {
                var now = DateTime.UtcNow;
                if (now >= nextAllowedTime)
                {
                    nextAllowedTime = now + sampleInterval;
                    modelChanged(model);
                }
                else
                {
                    latest = model;
                    hasValue = true;
                }
            }
        }
    }

    public void Dispose()
    {
        Subscriptions.ForEach(subscription => subscription.Dispose());
        Subscriptions.Clear();
    }

    sealed class CompositeDisposable(IDisposable[] items) : IDisposable
    {
        public void Dispose()
            => items.ForEach(item => item.Dispose());
    }
}
