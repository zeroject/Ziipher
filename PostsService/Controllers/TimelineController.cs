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

        public TimelineController(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        [HttpGet]
        [Route("GetTimeline/{userId}")]
        public IActionResult GetTimeline([FromRoute] int userId)
        {
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
            _timelineService.CreateTimeline(timeline);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteTimeline/{userId}")]
        public IActionResult DeleteTimeline([FromRoute] int userId)
        {
            _timelineService.DeleteTimeline(userId);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateTimeline/{timelineId}/{newUserID}")]
        public IActionResult UpdateTimeline([FromRoute] int timelineId, [FromRoute] int newUserID)
        {
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
            var timeline = _timelineService.GetTimelineByUser(userId);
            if (timeline == null)
            {
                return NotFound();
            }
            return Ok(timeline);
        }


    }
}
