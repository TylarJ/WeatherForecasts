using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecasts.Data.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddDbContext<WeatherForecastsContext>(options =>
        {
            options.UseInMemoryDatabase("WeatherForecasts");
        });

        return services;
    }
}
