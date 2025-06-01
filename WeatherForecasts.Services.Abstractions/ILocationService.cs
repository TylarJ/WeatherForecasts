using WeatherForecasts.Common;

namespace WeatherForecasts.Services.Abstractions;

public interface ILocationService
{
    Task<Location> Create(float latitude, float longitude);
    Task<IEnumerable<LocationDTO>> GetAll();
    Task<Location?> Get(float latitude, float longitude);
    Task<Location?> Get(int id);
    Task Delete(int id);
    Task<Location?> Update(int id);
}
