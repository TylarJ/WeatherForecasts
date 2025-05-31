namespace WeatherForecasts.Data.Abstractions;

public interface IForecastService
{
    Task Get(float latitude, float longitude);
}
