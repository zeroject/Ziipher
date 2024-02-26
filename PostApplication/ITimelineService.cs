using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostApplication
{
    public interface ITimelineService
    {

        /// <summary>
        /// Creates a new timeline
        /// </summary>
        /// <param name="newTimeline"></param>
        public void CreateTimeline(Timeline newTimeline);

        /// <summary>
        /// Returns all timelines
        /// </summary>
        /// <returns></returns>

        public List<Timeline> GetAllTimelines();

        /// <summary>
        /// Deletes a timeline
        /// </summary>
        /// <param name="timelineId"></param>

        public void DeleteTimeline(int timelineId);

        /// <summary>
        /// Updates a timeline
        /// </summary>
        /// <param name="timelineId"></param>
        /// <param name="newUserID"></param>

        public void UpdateTimeline(int timelineId, int newUserID);

        /// <summary>
        /// Gets a timeline by its id
        /// </summary>
        /// <param name="timelineId"></param>
        /// <returns></returns>

        public Timeline GetTimeline(int timelineId);

        /// <summary>
        /// Gets a timeline by its user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public Timeline GetTimelineByUser(int userId); 

    }
}
