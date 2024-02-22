using CommentApplication;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        public CommentController(ICommentCrud commentCrud)
        {
            _commentCrud = commentCrud;
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
            return Ok(_commentCrud.GetComments(pageSize, pageNumber, PostID));
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
            _commentCrud.AddComment(PostID, Comment);
            return Ok();
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
            _commentCrud.UpdateComment(CommentID, Comment);
            return Ok();
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
            _commentCrud.DeleteComment(CommentID);
            return Ok();
        }
    }
}
