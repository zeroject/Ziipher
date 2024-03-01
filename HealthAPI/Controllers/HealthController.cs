using Microsoft.AspNetCore.Mvc;

namespace HealthAPI.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/health")]
        public IActionResult Get(string service)
        {
            _logger.LogInformation("Request for health check resources for " + service);
            return Ok("Healthy");
        }
    }
}
