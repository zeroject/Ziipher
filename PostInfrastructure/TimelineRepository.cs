using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostInfrastructure
{
    public class TimelineRepository : ITimelineRepository
    {

        private readonly RepositoryDBContext _context;

        public TimelineRepository(RepositoryDBContext context)
        {
            _context = context;
        }

        public void CreateTimeline(Timeline newTimeline)
        {
            _context.Timelines.Add(newTimeline);
            _context.SaveChanges();
        }

        public void DeleteTimeline(int timelineId)
        {
            var timeline = _context.Timelines.Find(timelineId);
            if (timeline != null)
            {
                _context.Timelines.Remove(timeline);
                _context.SaveChanges();
            }
        }
        public List<Timeline> GetAllTimelines()
        {
            return _context.Timelines.ToList();
        }

        public Timeline GetTimeline(int timelineId)
        {
            var timeline = _context.Timelines.Find(timelineId);
            if (timeline == null)
            {
                throw new ArgumentException("Timeline not found.", nameof(timelineId));
            }
            return timeline;
        }
        public Timeline GetTimelineByUser(int userId)
        {
            var timeline = _context.Timelines.FirstOrDefault(t => t.UserID == userId);

            if (timeline == null)
            {
                throw new ArgumentException("Timeline not found for the user.", nameof(userId));
            }
            return timeline;
        }
        public void UpdateTimeline(int timelineId, int newUserID)
        {
            var timeline = _context.Timelines.Find(timelineId);
            if (timeline != null)
            {
                timeline.UserID = newUserID;
                _context.SaveChanges();
            }
        }
    }
}
