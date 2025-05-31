namespace WeatherForecasts.Services.Models;

internal class ForecastResponse
{
    public float latitude { get; set; }
    public float longitude { get; set; }
    public float generationtime_ms { get; set; }
    public int utc_offset_seconds { get; set; }
    public string timezone { get; set; }
    public string timezone_abbreviation { get; set; }
    public float elevation { get; set; }
    public Hourly_Units hourly_units { get; set; }
    public Hourly hourly { get; set; }
}

internal class Hourly_Units
{
    public string time { get; set; }
    public string temperature_2m { get; set; }
}

internal class Hourly
{
    public string[] time { get; set; }
    public float[] temperature_2m { get; set; }
}