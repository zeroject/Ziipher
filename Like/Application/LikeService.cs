using Domain;
using Microsoft.Extensions.Logging;

namespace LikeApplication;

public class LikeService : ILikeService
{
    ILikeRepository likeRepository;
    ILogger<LikeService> logger;


    public LikeService(ILikeRepository _likeRepository, ILogger<LikeService> _logger)
    {
        likeRepository = _likeRepository;
        logger = _logger;
    }

    public Like GetLikes(LikeDTO likeDTO)
    {
        logger.LogInformation("Getting likes for post with ID: " + likeDTO.PostID);
        return likeRepository.GetLikes(likeDTO);
    }

    public Like CreateLike(LikeDTO likeDTO)
    {
        logger.LogInformation("Adding like for post with ID: " + likeDTO.PostID);
        return likeRepository.CreateLike(likeDTO);
    }

    public Like AddLike(LikeDTO likeDTO)
    {
        logger.LogInformation("Updating like for post with ID: " + likeDTO.PostID);
        return likeRepository.AddLike(likeDTO);
    }

    public void RemoveLike(LikeDTO likeDTO)
    {
        logger.LogInformation("Removing like for post with ID: " + likeDTO.PostID);
        likeRepository.RemoveLike(likeDTO);
    }
}
