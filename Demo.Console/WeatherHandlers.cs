using static Foundation.WeatherCommands;

namespace Foundation;

sealed class WeatherHandlers(WeatherForecastService service) : ICommandHandler<LoadWeatherForecasts>
{
    public async Task On(LoadWeatherForecasts command, ICommandDispatcher dispatcher, CancellationToken cancellationToken)
        => dispatcher.Dispatch(new LoadWeatherForecastsResult(await service.GetForecasts(command.DateFrom, cancellationToken)));
}
