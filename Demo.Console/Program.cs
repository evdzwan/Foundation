using Foundation;
using Microsoft.Extensions.DependencyInjection;
using static Foundation.WeatherCommands;

var services = new ServiceCollection();
services.AddFoundation(config => config.Scan([typeof(Program).Assembly]))
        .AddScoped<WeatherForecastService>()
        .AddTransient<WeatherView>();

using var serviceProvider = services.BuildServiceProvider();
var view = serviceProvider.GetRequiredService<WeatherView>();

view.Initialize();
view.LoadForecasts();

await App.Run();

static class App
{
    static readonly TaskCompletionSource CompletionSource = new();

    public static Task Run()
        => CompletionSource.Task;

    public static void Stop()
        => CompletionSource.TrySetResult();
}

class WeatherView(IState<Weather> state, ICommandDispatcher dispatcher) : IDisposable
{
    readonly StateSubscriptions Subscriptions = new();

    public void Dispose()
        => Subscriptions.Dispose();

    public void Initialize()
        => Subscriptions.AddDebounced(state, OnWeatherChanged, TimeSpan.FromMilliseconds(300), emitImmediately: true);

    public void LoadForecasts()
        => dispatcher.Dispatch(new LoadWeatherForecasts(DateTime.Today));

    void OnWeatherChanged(Weather weather)
    {
        Console.WriteLine("Weather changed:");
        Console.WriteLine($"  Loading: {weather.Loading}");
        foreach (var forecast in weather.Forecasts)
        {
            Console.WriteLine($"  {forecast.Date:d}: {forecast.Temperature} °C");
        }

        if (weather is { Loading: false, Forecasts.Length: > 0 })
        {
            App.Stop();
        }
    }
}
