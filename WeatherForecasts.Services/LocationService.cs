using Microsoft.EntityFrameworkCore;
using WeatherForecasts.Common;
using WeatherForecasts.Data;
using WeatherForecasts.Services.Abstractions;

namespace WeatherForecasts.Services;

public class LocationService : ILocationService
{
    private readonly WeatherForecastsContext _dbContext;
    private readonly IWeatherProvider _weatherProvider;

    public LocationService(WeatherForecastsContext dbContext, IWeatherProvider weatherProvider)
    {
        _dbContext = dbContext;
        _weatherProvider = weatherProvider;
    }

    public async Task<IEnumerable<Location>> GetAll()
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
        LocationValidator.ValidateCoordinates(latitude, longitude);

        return await _dbContext.Locations
            .Include(l => l.Forecasts)
            .FirstOrDefaultAsync(l => l.Longitude == longitude && l.Latitude == latitude);
    }

    public async Task<Location> Create(float latitude, float longitude)
    {
        LocationValidator.ValidateCoordinates(latitude, longitude);

        var location = await Get(latitude, longitude);

        if (location != null)
            return location;

        var forecasts = await _weatherProvider.GetForecasts(latitude, longitude);

        location = new Location
        {
            Latitude = latitude,
            Longitude = longitude,
            Forecasts = forecasts
        };

        _dbContext.Locations.Add(location);
        await _dbContext.SaveChangesAsync();

        return location;
    }

    public async Task<Location?> Update(int id)
    {
        var location = await Get(id);

        if (location == null)
            return null;

        // Assuming we want to update the forecasts based on the current weather data
        var updatedForecasts = await _weatherProvider.GetForecasts(location.Latitude, location.Longitude);
        location.Forecasts = updatedForecasts;

        _dbContext.Locations.Update(location);
        await _dbContext.SaveChangesAsync();

        return location;
    }

    public async Task Delete(int id)
    {
        var location = await Get(id);
        
        if (location == null)
            throw new KeyNotFoundException($"Location with ID {id} not found.");

        _dbContext.Locations.Remove(location);
        await _dbContext.SaveChangesAsync();
    }
}
