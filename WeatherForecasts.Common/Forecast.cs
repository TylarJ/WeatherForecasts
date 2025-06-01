using System.Text.Json.Serialization;

namespace WeatherForecasts.Common;

public class Forecast
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public float TemperatureCelsius { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int LocationId { get; set; }

    [JsonIgnore] // HACK: Cleaner approach is to use a DTO or a separate model for serialization
    public Location Location { get; set; } = null!;
}
