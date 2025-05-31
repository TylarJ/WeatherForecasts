using WeatherForecasts.Common;

namespace WeatherForecasts.Services.Abstractions;

public interface ILocationService
{
    Task<Location> Create(float latitude, float longitude);
    Task<IEnumerable<Location>> GetAllLocations();
    Task<Location?> Get(float latitude, float longitude);
    Task<Location?> Get(int id);
}
