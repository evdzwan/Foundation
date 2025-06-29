using Foundation.Models;

namespace Foundation.Services;

sealed class WeatherForecastService : IWeatherForecastService
{
    static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    public async Task<WeatherForecast[]> GetForecasts(DateTime startDate)
    {
        await Task.Delay(500);
        return [.. Enumerable.Range(1, 5).Select(i => new WeatherForecast(Date: startDate.AddDays(i),
                                                                          TemperatureC: Random.Shared.Next(-20, 55),
                                                                          Summary: Summaries[Random.Shared.Next(Summaries.Length)]))];
    }
}
