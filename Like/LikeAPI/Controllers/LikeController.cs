using Domain;
using LikeApplication;
using Microsoft.AspNetCore.Mvc;

namespace LikeAPI;

[ApiController]
public class LikeController : ControllerBase
{
    private readonly ILikeService likeService;
    private readonly ILogger<LikeController> logger;
    public LikeController(ILogger<LikeController> _logger, ILikeService _likeService)
    {
        logger = _logger;
        likeService = _likeService;
    }

    [HttpGet]
    [Route("GetLikes")]
    [ProducesResponseType(typeof(Like), 200)]
    [ProducesResponseType(500)]
    public ActionResult<Like> GetLikes()
    {

        try
        {
            logger.LogInformation("Getting all likes");
            return Ok(likeService.GetLikes());
        } catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }

    [HttpGet]
    [Route("GetLike")]
    [ProducesResponseType(typeof(Like), 200)]
    [ProducesResponseType(500)]
    public ActionResult<Like> GetLike(int postId)
    {

        try
        {
            logger.LogInformation("Getting like for post with ID: " + postId);
            return Ok(likeService.GetLike(postId));
        } catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }


    [HttpPost]
    [Route("CreateLike")]
    [ProducesResponseType(typeof(Like), 200)]
    [ProducesResponseType(500)]
    public ActionResult<Like> CreateLike(LikeDTO likeDTO)
    {

        try
        {
            logger.LogInformation("Adding like for post with ID: " + likeDTO.PostID);
            return Ok(likeService.CreateLike(likeDTO));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }

    [HttpPut]
    [Route("AddLike")]
    [ProducesResponseType(typeof(Like), 200)]
    [ProducesResponseType(500)]
    public ActionResult<Like> AddLike(LikeDTO likeDTO)
    {

        try
        {
            logger.LogInformation("Updating like for post with ID: " + likeDTO.PostID);
            return Ok(likeService.AddLike(likeDTO));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }

    [HttpDelete]
    [Route("RemoveLike")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult RemoveLike(LikeDTO likeDTO)
    {

        try
        {
            logger.LogInformation("Removing like for post with ID: " + likeDTO.PostID);
            likeService.RemoveLike(likeDTO);
            return Ok();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }
}
