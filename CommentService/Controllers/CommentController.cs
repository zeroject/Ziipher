using CommentApplication;
using Domain;
using Domain.DTO_s;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers
{
    /// <summary>
    /// This Service is responsible for managing comments on posts.
    /// </summary>
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentCrud _commentCrud;
        private readonly ILogger<CommentController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        public CommentController(ICommentCrud commentCrud, ILogger<CommentController> logger)
        {
            _commentCrud = commentCrud;
            _logger = logger;
        }

        /// <summary>
        /// Returns all comments for a post with the given @PostID and paginates the results.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="PostID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetComments")]
        public IActionResult GetComments(int pageSize, int pageNumber, int PostID)
        {
            try
            {
                _logger.LogInformation("Getting comments for post with ID: " + PostID);
                return Ok(_commentCrud.GetComments(pageSize, pageNumber, PostID));
            } catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }

        /// <summary>
        /// Adds a comment to the post with the @commentDTO.
        /// </summary>
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddComment")]
        public IActionResult AddComment(CommentDTO commentDTO)
        {
            try
            {
                _logger.LogInformation("Adding comment to post with ID: " + commentDTO.PostID);
                _commentCrud.AddComment(commentDTO);
                return Ok();
            } catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }


        /// <summary>
        /// Updates the comment with the given @comment.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateComment")]
        public IActionResult UpdateComment(Comment comment)
        {
            try
            {
                _logger.LogInformation("Updating comment with ID: " + comment.CommentID);
                _commentCrud.UpdateComment(comment);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }

        /// <summary>
        /// Deletes the comment with the given @CommentID.
        /// </summary>
        /// <param name="CommentID"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteComment")]
        public IActionResult DeleteComment(int CommentID)
        {
            try
            {
                _logger.LogInformation("Deleting comment with ID: " + CommentID);
                _commentCrud.DeleteComment(CommentID);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }

        /// <summary>
        /// Deletes all comments for the post with the given @PostID.
        /// </summary>
        /// <param name="PostID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("MassDeleteComments")]
        public IActionResult MassDeleteComments(int PostID, int UserID)
        {
            try
            {
                _logger.LogInformation("Deleting all comments for post with ID: " + PostID);
                _commentCrud.MassDeleteComments(PostID, UserID);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }
    }
}
