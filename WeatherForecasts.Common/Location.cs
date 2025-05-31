namespace WeatherForecasts.Common;

public class Location
{
    public int Id { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Forecast> Forecasts { get; set; } = [];
}
