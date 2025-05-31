using Microsoft.EntityFrameworkCore;
using WeatherForecasts.Common;
using WeatherForecasts.Data;
using WeatherForecasts.Services.Abstractions;

namespace WeatherForecasts.Services;

public class LocationService : ILocationService
{
    private readonly WeatherForecastsContext _dbContext;

    public LocationService(WeatherForecastsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Location>> GetAllLocations()
    {
        return await _dbContext.Locations.ToListAsync();
    }

    public async Task<Location?> Get(int id)
    {
        return await _dbContext.Locations
            .Include(l => l.Forecasts)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Location?> Get(float latitude, float longitude)
    {
        if (longitude < -180 || longitude > 180)
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180 degrees.");
        if (latitude < -90 || latitude > 90)
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90 degrees.");

        return await _dbContext.Locations
            .Include(l => l.Forecasts)
            .FirstOrDefaultAsync(l => l.Longitude == longitude && l.Latitude == latitude);
    }

    public async Task<Location> Create(float latitude, float longitude)
    {
        var location = new Location
        {
            Latitude = latitude,
            Longitude = longitude
        };

        _dbContext.Locations.Add(location);
        await _dbContext.SaveChangesAsync();

        return location;
    }
}
