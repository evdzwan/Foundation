using System.Diagnostics.CodeAnalysis;

namespace Foundation;

// External
sealed class WeatherForecastService
{
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Demo")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Demo")]
    public async Task<WeatherForecast[]> GetForecasts(DateTime dateFrom, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        return [.. Enumerable.Range(0, 7).Select(i => new WeatherForecast(dateFrom.AddDays(i), Random.Shared.Next(minValue: -10, maxValue: 40)))];
    }
}
