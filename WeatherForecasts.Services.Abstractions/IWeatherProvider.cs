using WeatherForecasts.Common;

namespace WeatherForecasts.Services.Abstractions;

public interface IWeatherProvider
{
    Task<List<Forecast>> GetForecasts(float latitude, float longitude);
}
