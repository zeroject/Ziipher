using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimelineInfrastructure
{
    public interface ITimelineRepository
    {

        public void CreateTimeline(Timeline newTimeline);

        public List<Timeline> GetAllTimelines();

        public void DeleteTimeline(int timelineId);

        public void UpdateTimeline(int timelineId, int newUserID);

        public Timeline GetTimeline(int timelineId);

        public Timeline GetTimelineByUser(int userId);
    }
}
