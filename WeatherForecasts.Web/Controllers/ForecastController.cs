using Microsoft.AspNetCore.Mvc;
using WeatherForecasts.Common;
using WeatherForecasts.Services.Abstractions;
using WeatherForecasts.Web.Models;

namespace WeatherForecasts.Web.Controllers;

[ApiController]
[Route("locations/[controller]")]
public class ForecastController : ControllerBase
{
    private readonly IWeatherProvider _weatherProvider;

    public ForecastController(IWeatherProvider weatherProvider)
    {
        _weatherProvider = weatherProvider;
    }

    [HttpGet]
    public async Task<ActionResult<Location>> Get([FromQuery] LocationRequest location)
    {
        LocationValidator.ValidateCoordinates(location.Latitude, location.Longitude);

        var result = await _weatherProvider.GetForecasts(location.Latitude, location.Longitude);

        return Ok(result);
    }
}
