namespace TemperatureController.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController(ITemperatureMonitor monitor) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTemperatures()
        {
            var temps = monitor.GetTemperatures();

            return Ok(new{ temps.internalTemp, temps.externalTemp});
        }
    }
}
