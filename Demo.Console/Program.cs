using Foundation;
using Microsoft.Extensions.DependencyInjection;
using static Foundation.WeatherCommands;

var services = new ServiceCollection();
services.AddFoundation(config => config.Scan([typeof(Program).Assembly]))
        .AddScoped<WeatherForecastService>()
        .AddTransient<WeatherApp>();

using var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetRequiredService<WeatherApp>();
await app.Run();

class WeatherApp(IState<Weather> state, ICommandDispatcher dispatcher) : IDisposable
{
    readonly TaskCompletionSource CompletionSource = new();
    readonly StateSubscriptions Subscriptions = new();

    public async Task Run()
    {
        Initialize();
        LoadForecasts();
        await CompletionSource.Task;
    }

    void Initialize() => Subscriptions.AddDebounced(state, OnWeatherChanged, TimeSpan.FromMilliseconds(300), emitImmediately: true);
    void LoadForecasts() => dispatcher.Dispatch(new LoadWeatherForecasts(DateTime.Today));
    void Stop() => CompletionSource.TrySetResult();
    void IDisposable.Dispose() => Subscriptions.Dispose();

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
            Stop();
        }
    }
}
