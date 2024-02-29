using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostApplication;
using PostApplication.DTO_s;
using Microsoft.AspNetCore.Authorization;


namespace PostsService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TimelineController : ControllerBase
    {

        private readonly ITimelineService _timelineService;
        private readonly ILogger<TimelineController> _logger;
        public TimelineController(ITimelineService timelineService, ILogger<TimelineController> logger)
        {
            _timelineService = timelineService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetTimeline/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetTimeline([FromRoute] int userId)
        {
            _logger.LogInformation("Get the timeline from the user with id " + userId);
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
            try
            {
                var timeline = _timelineService.GetTimeline(userId);
                _logger.LogInformation("Timeline has been retrieved from the user");
                return Ok(timeline);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }

        [HttpPost]
        [Route("CreateTimeline")]
        [Authorize]
        public async Task<IActionResult> CreateTimeline([FromBody] PostTimelineDTO timeline)
        {
            _logger.LogInformation($"Created timeline: {timeline}");
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
            try
            {
                _timelineService.CreateTimeline(timeline);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Timeline couldn't be created");
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteTimeline/{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTimeline([FromRoute] int userId)
        {
            _logger.LogInformation($"Delete the timeline from the user with the id: {userId}");
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
            try
            {
                _timelineService.DeleteTimeline(userId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Timeline couldn't be delete");
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateTimeline/{timelineId}/{newUserID}")]
        [Authorize]
        public async Task<IActionResult> UpdateTimeline([FromRoute] int timelineId, [FromRoute] int newUserID)
        {
            _logger.LogInformation("Update the timeline with id" + timelineId + " for user with id" + newUserID);
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
            try
            {

                _timelineService.UpdateTimeline(timelineId, newUserID);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Timeline couldn't be updated");
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("GetAllTimelines")]
        public IActionResult GetAllTimelines()
        {
            var timelines = _timelineService.GetAllTimelines();
            return Ok(timelines);
        }

        [HttpGet]
        [Route("GetTimelineByUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetTimelineByUser([FromRoute] int userId)
        {
            _logger.LogInformation($"Get the timeline for the user: {userId}");
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
            try
            {
                var timeline = _timelineService.GetTimelineByUser(userId);

                return Ok(timeline);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Timeline couldn't be retrieved from the user");
                return StatusCode(500, e.Message);
            }
        }

        private async Task<bool> ValidateTokenAsync(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                _logger.LogError("Authorization token not found in request headers");
                return false;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://authservice:8080");
                var response = await client.PostAsJsonAsync($"/auth/validateUser?token={accessToken}", "");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error validating user to AuthWanabe");
                    return false;
                }
                var data = await response.Content.ReadFromJsonAsync<bool>();
                if (data != true)
                {
                    _logger.LogInformation("User failed to auth himself");
                    return false;
                }
                return true;
            }
        }
    }
}
