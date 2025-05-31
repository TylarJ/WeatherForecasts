using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using WeatherForecasts.Data.Abstractions;
using WeatherForecasts.Services.Configuration;
using WeatherForecasts.Services.Models;

namespace WeatherForecasts.Services;

public class ForecastService : IForecastService
{
    private readonly OpenMeteoConfiguration _config;
    private static HttpClient _httpClient;

    public ForecastService(IOptions<OpenMeteoConfiguration> config, IHttpClientFactory httpClientHandler)
    {
        _config = config.Value
            ?? throw new ArgumentNullException(nameof(config), $"Configuration missing for {nameof(OpenMeteoConfiguration)}.");

        _httpClient = httpClientHandler.CreateClient("OpenMeteoClient");
    }

    public async Task Get(float latitude, float longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90 degrees.");
        if (longitude < -180 || longitude > 180)
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180 degrees.");

        var requestUri = $"{_config.BaseUrl}/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m";

        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
        using var responseMessage = await _httpClient.SendAsync(requestMessage);
        var response = await responseMessage.Content.ReadFromJsonAsync<ForecastResponse>();
    }
}
