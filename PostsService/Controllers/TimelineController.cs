using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostApplication;
using PostApplication.DTO_s;

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
        public IActionResult GetTimeline([FromRoute] int userId)
        {
            _logger.LogInformation("Get the timeline from the user with id " + userId);
            var timeline = _timelineService.GetTimeline(userId);
            if (timeline == null)
            {
                return NotFound();
            }
            return Ok(timeline);
        }

        [HttpPost]
        [Route("CreateTimeline")]
        public IActionResult CreateTimeline([FromBody] PostTimelineDTO timeline)
        {
            _logger.LogInformation($"Created timeline: {timeline}");
            _timelineService.CreateTimeline(timeline);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteTimeline/{userId}")]
        public IActionResult DeleteTimeline([FromRoute] int userId)
        {
            _logger.LogInformation($"Delete the timeline from the user with the id: {userId}");
            _timelineService.DeleteTimeline(userId);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateTimeline/{timelineId}/{newUserID}")]
        public IActionResult UpdateTimeline([FromRoute] int timelineId, [FromRoute] int newUserID)
        {
            _logger.LogInformation("Update the timeline with id" + timelineId + " for user with id" + newUserID);
            _timelineService.UpdateTimeline(timelineId, newUserID);
            return Ok();
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
        public IActionResult GetTimelineByUser([FromRoute] int userId)
        {
            _logger.LogInformation($"Get the timeline for the user: {userId}");
            var timeline = _timelineService.GetTimelineByUser(userId);
            if (timeline == null)
            {
                return NotFound();
            }
            return Ok(timeline);
        }
    }
}
