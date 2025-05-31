using Microsoft.Extensions.Options;
using WeatherForecasts.Data.Abstractions;
using WeatherForecasts.Services.Configuration;

namespace WeatherForecasts.Services;

public class ForecastService : IForecastService
{
    private readonly OpenMeteoConfiguration _config;

    public ForecastService(IOptions<OpenMeteoConfiguration> config)
    {
        _config = config.Value
            ?? throw new ArgumentNullException(nameof(config), $"Configuration missing for {nameof(OpenMeteoConfiguration)}.");
    }

    public void Get(float latitude, float longitude)
    {
    }
}
