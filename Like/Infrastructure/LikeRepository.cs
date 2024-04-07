using Domain;
using LikeApplication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LikeInfrastructure;

public class LikeRepository : ILikeRepository
{
    private DbContextOptions<DbContext> options;
    private readonly ILogger<LikeRepository> logger;

    
    public LikeRepository(ILogger<LikeRepository> _logger)
    {
        logger = _logger;
        options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase("DMDB").Options;
    }

    public List<Like> GetLikes()
    {
        using (var context = new DbContext(options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            logger.LogInformation("Getting all likes");
            return context.Likes.ToList();
        }
    }

    public Like GetLike(int postId)
    {
        using (var context = new DbContext(options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            logger.LogInformation("Getting like for post with ID: " + postId);
            return context.Likes.Where(c => c.PostID == postId).FirstOrDefault();
        }
    }

    public Like CreateLike(Like like)
    {
        using (var context = new DbContext(options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            logger.LogInformation("Adding a new like to the database");  
            _ = context.Add(like);
            context.SaveChanges();
            return like;
        }
    }

    public Like AddLike(LikeDTO likeDTO)
    {
        using (var context = new DbContext(options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            logger.LogInformation("Updating a like in the database");
            Like likeToUpdate = context.Likes.Where(c => c.PostID == likeDTO.PostID).FirstOrDefault();
            likeToUpdate.UserIDs.Add(likeDTO.UserID.Value);
            _ = context.Likes.Update(likeToUpdate);
            context.SaveChanges();
            return likeToUpdate;
        }
    }

    public void RemoveLike(LikeDTO likeDTO)
    {
        using (var context = new DbContext(options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            logger.LogInformation("Removing a like from the database");
            Like likeToRemove = context.Likes.Where(c => c.PostID == likeDTO.PostID).FirstOrDefault();
            likeToRemove.UserIDs.Remove(likeDTO.UserID.Value);
            _ = context.Likes.Update(likeToRemove);
            context.SaveChanges();
        }
    }   
}
