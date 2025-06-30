namespace Foundation;

static class WeatherCommands
{
    public record LoadWeatherForecasts(DateTime DateFrom);
    public record LoadWeatherForecastsResult(WeatherForecast[] Forecasts);
}
