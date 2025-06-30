namespace Foundation;

record Weather(bool Loading, WeatherForecast[] Forecasts) : ICreateNew<Weather>
{
    public static Weather CreateNew()
        => new(Loading: false, Forecasts: []);
}
