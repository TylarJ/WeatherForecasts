namespace WeatherForecasts.Common;

public class Forecast
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public float TemperatureCelsius { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
}
