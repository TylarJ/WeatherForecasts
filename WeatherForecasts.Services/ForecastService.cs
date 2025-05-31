using WeatherForecasts.Common;
using WeatherForecasts.Services.Abstractions;

namespace WeatherForecasts.Services;

public class ForecastService : IForecastService
{
    private readonly IWeatherProvider _weatherProvider;

    public ForecastService(IWeatherProvider weatherProvider)
    {
        _weatherProvider = weatherProvider;
    }

    public async Task<List<Forecast>> Get(float latitude, float longitude)
    {
        return await _weatherProvider.GetForecasts(latitude, longitude);
    }
}
