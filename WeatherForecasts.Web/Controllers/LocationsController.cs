using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherForecasts.Common;
using WeatherForecasts.Services.Abstractions;
using WeatherForecasts.Web.Models;

namespace WeatherForecasts.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Location>>> GetAll()
    {
        return await _locationService.GetAllLocations()
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    return StatusCode(500, "Internal server error: " + task.Exception?.Message);
                }
                if (task.Result == null || !task.Result.Any())
                {
                    return NotFound("No locations found.");
                }
                return Ok(task.Result);
            });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Location>> GetByLocation(int id)
    {
        return await _locationService.Get(id)
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    return StatusCode(500, "Internal server error: " + task.Exception?.Message);
                }

                if (task.Result == null)
                {
                    return NotFound("Location not found.");
                }
                return Ok(task.Result);
            });
    }

    [HttpGet("find")]
    public async Task<ActionResult<Location>> GetByLocation([FromQuery] LocationRequest location)
    {
        return await _locationService.Get(location.Latitude, location.Longitude)
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    return StatusCode(500, "Internal server error: " + task.Exception?.Message);
                }

                if (task.Result == null)
                {
                    return NotFound("Location not found.");
                }
                return Ok(task.Result);
            });
    }

    [HttpPost]
    public async Task<ActionResult<Location>> Post(LocationRequest location)
    {
        return await _locationService.Create(location.Latitude, location.Longitude)
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    return StatusCode(500, "Internal server error: " + task.Exception?.Message);
                }

                return CreatedAtAction(nameof(GetByLocation), new { latitude = location.Latitude, longitude = location.Longitude }, task.Result);
            });
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] LocationRequest location)
    {
        // TODO: Delete specified location
        return Ok();
    }
}
