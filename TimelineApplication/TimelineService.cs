using AutoMapper;
using Domain;
using TimelineApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimelineInfrastructure;

namespace TimelineApplication
{
    public class TimelineService : ITimelineService
    {

        private ITimelineRepository _timelineRepository;
        private readonly IMapper _mapper;


        public TimelineService(ITimelineRepository timelineRepository, IMapper mapper)
        {
            _timelineRepository = timelineRepository;
            _mapper = mapper;
        }


        public void CreateTimeline(PostTimelineDTO newTimeline)
        {
            _timelineRepository.CreateTimeline(_mapper.Map<Timeline>(newTimeline));
        }
        public void DeleteTimeline(int timelineId)
        {
            _timelineRepository.DeleteTimeline(timelineId);
        }

        public List<Timeline> GetAllTimelines()
        {
            return _timelineRepository.GetAllTimelines();
        }

        public Timeline GetTimeline(int timelineId)
        {
            return _timelineRepository.GetTimeline(timelineId);
        }

        public Timeline GetTimelineByUser(int userId)
        {
            return _timelineRepository.GetTimelineByUser(userId);
        }

        public void UpdateTimeline(int timelineId, int newUserID)
        {
            _timelineRepository.UpdateTimeline(timelineId, newUserID);
        }

        public async Task AddPostToTimeline(PostAddTimeline newPost)
        {
            await _timelineRepository.AddPostToTimeline(_mapper.Map<Post>(newPost));
        }
    }
}
