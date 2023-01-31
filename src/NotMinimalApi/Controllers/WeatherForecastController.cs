using Microsoft.AspNetCore.Mvc;

namespace NotMinimalApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Get()
    {
        return Ok("Hello, World!");
    }
}
