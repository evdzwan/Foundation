using Foundation.Models;

namespace Foundation.Services;

interface IWeatherForecastService
{
    Task<WeatherForecast[]> GetForecasts(DateTime startDate);
}
