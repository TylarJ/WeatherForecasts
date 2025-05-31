namespace WeatherForecasts.Data.Abstractions;

public interface IForecastService
{
    void Get(float latitude, float longitude);
}
