using Microsoft.EntityFrameworkCore;

namespace WeatherForecasts.Data;

public class WeatherForecastsContext : DbContext
{

    public WeatherForecastsContext() : base() { }
    public WeatherForecastsContext(DbContextOptions<WeatherForecastsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
