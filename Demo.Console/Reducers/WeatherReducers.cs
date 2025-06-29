using Fluxor;
using Foundation.Actions;
using Foundation.States;

namespace Foundation.Reducers;

static class WeatherReducers
{
    [ReducerMethod(typeof(WeatherActions.FetchData))]
    public static WeatherState FetchData(WeatherState state)
        => new(Loading: true, Forecasts: []);

    [ReducerMethod]
    public static WeatherState FetchDataResult(WeatherState state, WeatherActions.FetchDataResult action)
        => new(Loading: false, Forecasts: action.Forecasts);

    [Reducer]
    public static WeatherState InvalidWithoutAction(WeatherState state)
        => new(Loading: true, Forecasts: []);

    [Reducer]
    public static WeatherState InvalidWithoutState(WeatherActions.FetchDataResult action)
        => new(Loading: true, Forecasts: []);

    [Reducer]
    public static WeatherState InvalidWithAdditionalParameter(WeatherState state, WeatherActions.FetchDataResult action, int value)
        => new(Loading: true, Forecasts: []);

    [Reducer]
    public static void InvalidWithoutResult(WeatherState state, WeatherActions.FetchDataResult action)
        => throw new InvalidOperationException();

    [Reducer]
    public static string InvalidWithWrongResult(WeatherState state, WeatherActions.FetchDataResult action)
        => throw new InvalidOperationException();

    [Reducer]
    public static WeatherState Valid(WeatherState state, WeatherActions.FetchDataResult action)
        => new(Loading: false, Forecasts: action.Forecasts);
}
