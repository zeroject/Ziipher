using AutoMapper;
using Domain;
using Messaging;
using Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace LikeApplication;

public class LikeService : ILikeService
{
    ILikeRepository likeRepository;
    ILogger<LikeService> logger;
    MessageClient _messageClient;
    private IMapper _mapper;
    


    public LikeService(ILikeRepository _likeRepository, ILogger<LikeService> _logger, MessageClient messageClient, IMapper mapper)
    {
        likeRepository = _likeRepository;
        logger = _logger;
        _messageClient = messageClient;
        _mapper = mapper;
    }

    public List<Like> GetLikes()
    {
        logger.LogInformation("Getting all likes");
        return likeRepository.GetLikes();
    }

    public Like GetLike(int postId)
    {
        logger.LogInformation("Getting like for post with ID: " + postId);

        return likeRepository.GetLike(postId);
    }

    public async Task<Like> CreateLike(LikeDTO likeDTO)
    {
        logger.LogInformation("Adding like for post with ID: " + likeDTO.PostID);
        var like = await likeRepository.CreateLike(_mapper.Map<Like>(likeDTO));
        await _messageClient.Send(new AddLikeIfCreated("Adding like to post", like.ID , likeDTO.PostID), "AddLikeToPost");
        return like;
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
