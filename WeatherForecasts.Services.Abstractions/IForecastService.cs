using WeatherForecasts.Common;

namespace WeatherForecasts.Services.Abstractions;

public interface IForecastService
{
    Task<List<Forecast>> Get(float latitude, float longitude);
}
