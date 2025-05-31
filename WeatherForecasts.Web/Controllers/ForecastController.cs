using Microsoft.AspNetCore.Mvc;
using WeatherForecasts.Common;
using WeatherForecasts.Services.Abstractions;
using WeatherForecasts.Web.Models;

namespace WeatherForecasts.Web.Controllers;

[ApiController]
[Route("locations/[controller]")]
public class ForecastController : ControllerBase
{
    private readonly IForecastService _forecastService;

    public ForecastController(IForecastService forecastService)
    {
        _forecastService = forecastService;
    }

    [HttpGet]
    public async Task<ActionResult<Location>> Get([FromQuery] LocationRequest location)
    {
        var result = await _forecastService.Get(location.Latitude, location.Longitude);

        return Ok(result);
    }
}
