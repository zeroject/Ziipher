using Domain;
using HealthApplication;
using Microsoft.AspNetCore.Mvc;

namespace HealthAPI.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly IHealthService _healthService;
        public HealthController(ILogger<HealthController> logger, IHealthService healthService)
        {
            _logger = logger;
            _healthService = healthService;
        }

        [HttpGet]
        [Route("health")]
        public IActionResult Get()
        {
            _logger.LogInformation("Request for health check resources for ");
            return Ok(_healthService.GetHealth());
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult ping()
        {
            _logger.LogInformation("Request for ping check");
            return Ok("pong");
        }

        [HttpPost]
        [Route("health")]
        public IActionResult Post(Health service)
        {
            _logger.LogInformation("Request for health check resources for " + service);
            try 
            {
                _healthService.PostHealth(service);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error posting health check resources for " + service.ServiceName, ex);
                return StatusCode(500, "Error posting health check resources for " + service.ServiceName);
            }
            return Ok();
        }
    }
}
