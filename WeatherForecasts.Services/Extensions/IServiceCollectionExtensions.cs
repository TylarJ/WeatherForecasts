﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecasts.Services.Abstractions;
using WeatherForecasts.Services.Configuration;

namespace WeatherForecasts.Services.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddTransient<ILocationService, LocationService>()
            .AddTransient<IWeatherProvider, OpenMeteoWeatherProvider>()
            .AddHttpClient()
            .Configure<OpenMeteoConfiguration>(config.GetSection(nameof(OpenMeteoConfiguration)));
    }
}
