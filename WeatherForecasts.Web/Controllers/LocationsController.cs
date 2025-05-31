using Microsoft.AspNetCore.Mvc;
using WeatherForecasts.Common;
using WeatherForecasts.Web.Models;

namespace WeatherForecasts.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Location>> Get([FromQuery] LocationRequest location)
    {
        // TODO: Return all locations...
        return Ok();
    }

    [HttpPost]
    public ActionResult<Location> Post(LocationRequest location)
    {
        // TODO: Post new location
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] LocationRequest location)
    {
        // TODO: Delete specified location
        return Ok();
    }
}
