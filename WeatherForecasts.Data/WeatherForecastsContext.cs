using Microsoft.EntityFrameworkCore;
using WeatherForecasts.Common;

namespace WeatherForecasts.Data;

public class WeatherForecastsContext : DbContext
{
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Forecast> Forecasts { get; set; } = null!;

    public WeatherForecastsContext() : base() { }
    public WeatherForecastsContext(DbContextOptions<WeatherForecastsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>()
            .HasMany(l => l.Forecasts)
            .WithOne(f => f.Location)
            .HasForeignKey(f => f.LocationId);
    }
}
