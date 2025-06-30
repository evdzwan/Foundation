using Microsoft.Extensions.DependencyInjection;

namespace Foundation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoundation(this IServiceCollection @this)
    {
        throw new NotImplementedException();
    }

    //public static IServiceCollection AddFoundation(this IServiceCollection @this, Action<IFoundationConfiguration>? config = null)
    //{
    //    var configuration = new FoundationConfiguration(@this);
    //    config?.Invoke(configuration);
    //    return @this;
    //}
}

record Weather(bool Loading, WeatherForecast[] Forecasts);
record WeatherForecast(DateTime Date, int Temperature); // External
interface IWeatherService // External
{
    Task<WeatherForecast[]> GetForecasts(DateTime dateFrom, CancellationToken cancellationToken);
}

record LoadWeatherForecasts(DateTime DateFrom);
record LoadWeatherForecastsResult(WeatherForecast[] Forecasts);

class WeatherMutators : IMutator<Weather, LoadWeatherForecasts>,
                        IMutator<Weather, LoadWeatherForecastsResult>
{
    public Weather On(Weather model, LoadWeatherForecasts command)
        => new(Loading: true, Forecasts: []);

    public Weather On(Weather model, LoadWeatherForecastsResult result)
        => new(Loading: false, result.Forecasts);
}

class WeatherHandlers(IWeatherService service) : ICommandHandler<LoadWeatherForecasts>
{
    public async Task On(LoadWeatherForecasts command, ICommandDispatcher dispatcher, CancellationToken cancellationToken)
        => dispatcher.Dispatch(new LoadWeatherForecastsResult(await service.GetForecasts(command.DateFrom, cancellationToken)));
}

class WeatherView(IState<Weather> state, ICommandDispatcher dispatcher) : IDisposable
{
    readonly StateSubscriptions Subscriptions = new();
    public void Dispose() => Subscriptions.Dispose();
    public void Initialize() => Subscriptions.AddDebounced(state, OnWeatherChanged, TimeSpan.FromMilliseconds(300), emitImmediately: true);
    public void LoadForecasts() => dispatcher.Dispatch(new LoadWeatherForecasts(DateTime.Today));
    void OnWeatherChanged(Weather weather) => Console.WriteLine($"Weather changed: {weather}");
}
