using Fluxor;
using Foundation.Models;

namespace Foundation.States;

[FeatureState]
sealed record WeatherState(bool Loading, WeatherForecast[] Forecasts)
{
    private WeatherState() : this(Loading: false, Forecasts: []) { }
}
