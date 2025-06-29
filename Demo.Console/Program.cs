using Fluxor;
using Foundation.Actions;
using Foundation.Services;
using Foundation.States;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostApplicationBuilder(args);
builder.Services.AddHostedService<Worker>()
                .AddScoped<WeatherRetriever>()
                .AddScoped<IWeatherForecastService, WeatherForecastService>()
                .AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));

var host = builder.Build();
host.Run();

class Worker(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Worker>>();

        var store = scope.ServiceProvider.GetRequiredService<IStore>();
        await store.InitializeAsync();

        var retriever = scope.ServiceProvider.GetRequiredService<WeatherRetriever>();
        await retriever.Retrieve(stoppingToken);
    }
}

class WeatherRetriever(IState<WeatherState> state, IDispatcher dispatcher, ILogger<WeatherRetriever> logger)
{
    public async Task Retrieve(CancellationToken cancellationToken)
    {
        state.StateChanged += OnStateChanged;

        try
        {
            dispatcher.Dispatch(new WeatherActions.FetchData());

            var completionSource = new TaskCompletionSource();
            cancellationToken.Register(completionSource.SetResult);
            await completionSource.Task;
        }
        finally
        {
            state.StateChanged -= OnStateChanged;
        }
    }

    void OnStateChanged(object? sender, EventArgs e)
    {
        logger.LogInformation("Weather is {Loading}", [state.Value.Loading ? "Loading" : "Loaded"]);
        foreach (var forecast in state.Value.Forecasts)
        {
            logger.LogInformation("  {Forecast}", [forecast]);
        }
    }
}
