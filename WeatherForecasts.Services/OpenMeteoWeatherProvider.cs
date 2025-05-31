using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using WeatherForecasts.Common;
using WeatherForecasts.Services.Abstractions;
using WeatherForecasts.Services.Configuration;
using WeatherForecasts.Services.Models;

namespace WeatherForecasts.Services;

public class OpenMeteoWeatherProvider : IWeatherProvider
{
    private readonly HttpClient _httpClient;
    private readonly OpenMeteoConfiguration _config;

    public OpenMeteoWeatherProvider(IOptions<OpenMeteoConfiguration> config, IHttpClientFactory httpClientFactory)
    {
        _config = config.Value ?? throw new ArgumentNullException(nameof(config));
        _httpClient = httpClientFactory.CreateClient("OpenMeteoClient");
    }

    public async Task<List<Forecast>> GetForecasts(float latitude, float longitude)
    {
        var requestUri = $"{_config.BaseUrl}/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m";

        var responseMessage = await _httpClient.GetAsync(requestUri);
        var response = await responseMessage.Content.ReadFromJsonAsync<ForecastResponse>()
            ?? throw new HttpRequestException("Failed to retrieve forecast data.");

        return response.hourly?.time != null && response.hourly.temperature_2m != null
            ? response.hourly.time.Select((t, i) => new Forecast
            {
                Date = DateTime.Parse(t),
                TemperatureCelsius = response.hourly.temperature_2m[i],
                CreatedAt = DateTime.UtcNow
            })
                .ToList()
            : new List<Forecast>();
    }
}
