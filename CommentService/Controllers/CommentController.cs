using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers
{
    /// <summary>
    /// This Service is responsible for managing comments on posts.
    /// </summary>
    [ApiController]
    public class CommentController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        public CommentController()
        {
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
            return Ok("GetComments");
        }

        /// <summary>
        /// Adds a comment to the post with the @PostID.
        /// </summary>
        /// <param name="PostID"></param>
        /// <param name="Comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddComment")]
        public IActionResult AddComment(int PostID, string Comment)
        {
            return Ok("AddComment");
        }


        /// <summary>
        /// Updates the comment with the given @CommentID.
        /// </summary>
        /// <param name="CommentID"></param>
        /// <param name="Comment"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateComment")]
        public IActionResult UpdateComment(int CommentID, string Comment)
        {
            return Ok("UpdateComment");
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
            return Ok("DeleteComment");
        }
    }
}
