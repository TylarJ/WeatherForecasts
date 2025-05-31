using WeatherForecasts.Common;

namespace WeatherForecasts.Data.Abstractions;

public interface IForecastService
{
    Task<Location> Get(float latitude, float longitude);
}
