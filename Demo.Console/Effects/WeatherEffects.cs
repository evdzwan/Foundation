using Fluxor;
using Foundation.Actions;
using Foundation.Services;

namespace Foundation.Effects;

sealed class WeatherEffects(IWeatherForecastService service)
{
    [EffectMethod(typeof(WeatherActions.FetchData))]
    public async Task FetchData(IDispatcher dispatcher)
    {
        var forecasts = await service.GetForecasts(DateTime.Today);
        dispatcher.Dispatch(new WeatherActions.FetchDataResult(forecasts));
    }

    [Effect]
    public Task InvalidWithoutAction(IDispatcher dispatcher)
        => throw new NotImplementedException();

    [Effect]
    public Task InvalidWithoutDispatcher(WeatherActions.FetchData action)
        => throw new NotImplementedException();

    [Effect]
    public Task InvalidWithAdditionalParameter(WeatherActions.FetchData action, IDispatcher dispatcher, int value)
        => throw new NotImplementedException();

    [Effect]
    public Task InvalidWithNonDispatcherParameter(WeatherActions.FetchData action, string dispatcher)
        => throw new NotImplementedException();

    [Effect]
    public void ValidWithVoid(WeatherActions.FetchData action, IDispatcher dispatcher)
        => throw new NotImplementedException();

    [Effect]
    public Task ValidWithTask(WeatherActions.FetchData action, IDispatcher dispatcher)
        => throw new NotImplementedException();
}
