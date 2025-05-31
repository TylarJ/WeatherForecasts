using WeatherForecasts.Common;

namespace WeatherForecasts.Services.Abstractions;

public interface IForecastService
{
    Task<Location> Get(float latitude, float longitude);
}
