using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TimelineApplication;
using TimelineApplication.DTO;


namespace TimelineController.Controllers
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
        [Route("GetTimeline")]
        [Authorize]
        public async Task<IActionResult> GetTimeline([FromBody] GetTimelineDTO getTimeline)
        {
            _logger.LogInformation("Get the timeline from the user with id " + getTimeline.UserID);
            try
            {
                var timeline = _timelineService.GetTimeline(getTimeline.UserID);
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
        [Route("DeleteTimeline")]
        [Authorize]
        public async Task<IActionResult> DeleteTimeline([FromBody] DeleteTimelineDTO deleteTimeline)
        {
            try
            {
                _timelineService.DeleteTimeline(deleteTimeline.UserId);
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
        public async Task<IActionResult> UpdateTimeline([FromBody] TimelineUpdateDTO timelineUpdate)
        {
            _logger.LogInformation("Update the timeline with id" + timelineUpdate.TimelineID + " for user with id" + timelineUpdate.NewUserID);
            try
            {

                _timelineService.UpdateTimeline(timelineUpdate.TimelineID, timelineUpdate.NewUserID);
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
        [Route("GetTimelineByUser")]
        [Authorize]
        public async Task<IActionResult> GetTimelineByUser([FromBody] GetTimelineByUserDTO getTimelineByUser)
        {
            _logger.LogInformation($"Get the timeline for the user: {getTimelineByUser.UserID}");
            try
            {
                var timeline = _timelineService.GetTimelineByUser(getTimelineByUser.UserID);

                return Ok(timeline);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Timeline couldn't be retrieved from the user");
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("AddPostToTimeline")]
        [Authorize]
        public async Task<IActionResult> AddPostToTimeline([FromBody] PostAddTimeline post)
        {
            _logger.LogInformation($"Add post to the timeline with id: {post.TimelineID}");
            try
            {
                _timelineService.AddPostToTimeline(post);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Post couldn't be added to the timeline");
                return StatusCode(500, e.Message);
            }
        }
    }
}
