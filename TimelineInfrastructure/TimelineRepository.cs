using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimelineInfrastructure
{
    public class TimelineRepository : ITimelineRepository
    {

        private DbContextOptions<RepositoryDBContext> _options;

        public TimelineRepository()
        {
            _options = new DbContextOptionsBuilder<RepositoryDBContext>().UseInMemoryDatabase("TimelineDB").Options;
        }

        public void CreateTimeline(Timeline newTimeline)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                context.Timelines.Add(newTimeline);
                context.SaveChanges();
            }
       
        }

        public void DeleteTimeline(int timelineId)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                var timeline = context.Timelines.Find(timelineId);
                if (timeline != null)
                {
                    context.Timelines.Remove(timeline);
                    context.SaveChanges();
                }
            }

        }
        public List<Timeline> GetAllTimelines()
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                return context.Timelines.ToList();
            }
        }

        public Timeline GetTimeline(int timelineId)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                var timeline = context.Timelines.Find(timelineId);
                if (timeline == null)
                {
                    throw new ArgumentException("Timeline not found.", nameof(timelineId));
                }
                return timeline;
            }
        }
        public Timeline GetTimelineByUser(int userId)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                var timeline = context.Timelines.FirstOrDefault(t => t.UserID == userId);

                if (timeline == null)
                {
                    throw new ArgumentException("Timeline not found for the user.", nameof(userId));
                }
                return timeline;
            }
        }
        public void UpdateTimeline(int timelineId, int newUserID)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                var timeline = context.Timelines.Find(timelineId);
                if (timeline != null)
                {
                    timeline.UserID = newUserID;
                    context.SaveChanges();
                }
            }
        }
        public async Task AddPostToTimeline(Post newPost)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                var timeline = context.Timelines.Find(newPost.TimelineID);
                if (timeline == null)
                {
                    throw new ArgumentException("Timeline not found.", nameof(newPost.TimelineID));
                }
                timeline.PostIDs.Add(newPost.PostID);
                await context.SaveChangesAsync();
            }
        }
    }
}
