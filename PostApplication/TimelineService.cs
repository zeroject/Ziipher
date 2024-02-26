using Domain;
using PostInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostApplication
{
    public class TimelineService : ITimelineService
    {

        ITimelineRepository _timelineRepository;

        public TimelineService(ITimelineRepository timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }


        public void CreateTimeline(Timeline newTimeline)
        {
            _timelineRepository.CreateTimeline(newTimeline);
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
    }
}
