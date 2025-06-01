using Microsoft.EntityFrameworkCore;
using Moq;
using WeatherForecasts.Common;
using WeatherForecasts.Data;
using WeatherForecasts.Services.Abstractions;
using Xunit;

namespace WeatherForecasts.Services.Tests;

public class LocationServiceTests
{
    [Fact]
    public async Task GetLocation_ValidId_ReturnsMatchingLocation()
    {
        // Arrange
        var expectedId = 1;

        using WeatherForecastsContext context = GetDatabase();
        context.Locations.Add(new Location { Id = expectedId, Latitude = 10.0f, Longitude = 20.0f });
        await context.SaveChangesAsync();

        var weatherProviderMock = new Mock<IWeatherProvider>();

        var uut = new LocationService(context, weatherProviderMock.Object);


        // Act
        var result = await uut.Get(expectedId);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedId, result.Id);
    }

    [Fact]
    public async Task GetLocation_ValidLatLong_ReturnsMatchingLocation()
    {
        // Arrange
        var expectedLat = 10f;
        var expectedLong = 20f;

        using WeatherForecastsContext context = GetDatabase();
        context.Locations.Add(new Location { Id = 1, Latitude = expectedLat, Longitude = expectedLong });
        await context.SaveChangesAsync();

        var weatherProviderMock = new Mock<IWeatherProvider>();

        var uut = new LocationService(context, weatherProviderMock.Object);


        // Act
        var result = await uut.Get(expectedLat, expectedLong);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedLong, result.Longitude);
    }

    [Fact]
    public async Task CreateLocation_DuplicateLatLong_ReturnsExistingLocation()
    {
        // Arrange
        var existingLocation = new Location { Id = 1, Latitude = 10.0f, Longitude = 20.0f };

        using var context = GetDatabase();
        context.Locations.Add(existingLocation);
        await context.SaveChangesAsync();

        var weatherProviderMock = new Mock<IWeatherProvider>();
        var uut = new LocationService(context, weatherProviderMock.Object);


        // Act
        var result = await uut.Create(existingLocation.Latitude, existingLocation.Longitude);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingLocation.Id, result.Id);
    }

    [Fact]
    public async Task UpdateLocation_UpdatesForecasts()
    {
        // Arrange
        var location = new Location { Id = 1, Latitude = 10.0f, Longitude = 20.0f, Forecasts = new List<Forecast>() };

        using var context = GetDatabase();
        context.Locations.Add(location);
        await context.SaveChangesAsync();

        var weatherProviderMock = new Mock<IWeatherProvider>();
        weatherProviderMock.Setup(wp => wp.GetForecasts(location.Latitude, location.Longitude))
            .ReturnsAsync(new List<Forecast>
            {
                new Forecast { Date = DateTime.UtcNow, TemperatureCelsius = 25.0f }
            });

        var uut = new LocationService(context, weatherProviderMock.Object);


        // Act
        var updatedLocation = await uut.Update(location.Id);


        // Assert
        Assert.NotNull(updatedLocation);
        Assert.Single(updatedLocation.Forecasts); // Forecasts should be updated
        Assert.Equal(25.0f, updatedLocation.Forecasts[0].TemperatureCelsius); // Check new forecast data
    }

    [Fact]
    public async Task DeleteLocation_ValidId_RemovesLocationFromDatabase()
    {
        // Arrange
        var location = new Location { Id = 1, Latitude = 10.0f, Longitude = 20.0f };

        using var context = GetDatabase();
        context.Locations.Add(location);
        await context.SaveChangesAsync();

        var weatherProviderMock = new Mock<IWeatherProvider>();
        var uut = new LocationService(context, weatherProviderMock.Object);


        // Act
        await uut.Delete(location.Id);
        var deletedLocation = await context.Locations.FindAsync(location.Id);


        // Assert
        Assert.Null(deletedLocation); // Location should be removed from DB
    }

    [Fact]
    public async Task DeleteLocation_InvalidId_ThrowsKeyNotFoundException()
    {
        // Arrange
        using var context = GetDatabase();
        var weatherProviderMock = new Mock<IWeatherProvider>();
        var uut = new LocationService(context, weatherProviderMock.Object);


        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => uut.Delete(999)); // Non-existent ID
        Assert.Equal("Location with ID 999 not found.", exception.Message);
    }

    private static WeatherForecastsContext GetDatabase()
    {
        var options = new DbContextOptionsBuilder<WeatherForecastsContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new WeatherForecastsContext(options);
        return context;
    }
}
