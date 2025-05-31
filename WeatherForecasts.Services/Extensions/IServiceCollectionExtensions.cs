using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecasts.Data.Abstractions;
using WeatherForecasts.Services.Configuration;

namespace WeatherForecasts.Services.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddSingleton<IForecastService, ForecastService>()
            .AddHttpClient()
            .Configure<OpenMeteoConfiguration>(config.GetSection(nameof(OpenMeteoConfiguration)));
    }
}
