using Foundation.Models;

namespace Foundation.Actions;

static class WeatherActions
{
    public sealed record FetchData();
    public sealed record FetchDataResult(WeatherForecast[] Forecasts);
}
