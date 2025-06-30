using static Foundation.WeatherCommands;

namespace Foundation;

sealed class WeatherMutators : IMutator<Weather, LoadWeatherForecasts>,
                               IMutator<Weather, LoadWeatherForecastsResult>
{
    public Weather On(Weather model, LoadWeatherForecasts command)
        => new(Loading: true, Forecasts: []);

    public Weather On(Weather model, LoadWeatherForecastsResult result)
        => new(Loading: false, result.Forecasts);
}
