using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostApplication;
using PostApplication.DTO_s;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostsService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly ILogger<PostController> _logger;

        private readonly IPostService _postService;

        public PostController(IPostService postService, ILogger<PostController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllPosts")]
        public ActionResult<Dictionary<Post, Like>> GetAllPosts( int timelineId)
        {
            _logger.LogInformation("Getting posts for " + timelineId);
            var posts = _postService.GetAllPosts(timelineId);
            return Ok(posts);
        }


        [HttpGet]
        [Route("GetPost")]
        public ActionResult<Dictionary<Post, Like>> GetPost([FromBody] GetPostDTO getPost)
        {
            try
            {
                _logger.LogInformation("Get the post with ID " + getPost.PostID + "from timeline with id " + getPost.TimelineID);
                var post = _postService.GetPost(getPost.TimelineID, getPost.PostID);             
                return Ok(post);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Post couldn't be retrieved");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePost")]
        [Authorize]
        public IActionResult DeletePost([FromBody] DeletePostDTO deletePost)
        {
            try
            {
                _logger.LogInformation("Delete post with id " + deletePost.PostID + "from timeline with id " + deletePost.TimelineID);
                _postService.DeletePost(deletePost.TimelineID, deletePost.PostID);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post couldn't be deleted");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePost")]
        [Authorize]
        public IActionResult UpdatePost(PostUpdateDTO postUpdate)
        {
            try
            {
                _logger.LogInformation("Update post with id " + postUpdate.PostID + "from timeline with id" + postUpdate.TimelineID + "with the updated post " + postUpdate);
                _postService.UpdatePost(postUpdate.TimelineID, postUpdate.PostID, postUpdate.Text, postUpdate.PostDate);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Post couldn't be updated");
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("GetPostsByUser/{timelineId}/{userId}")]
        [Authorize]
        public IActionResult GetPostsByUser([FromBody] GetPostByUserDTO getPostByUser)
        {
            _logger.LogInformation("Get the posts from timeline with the id " + getPostByUser.TimelineID + "from the user with id" + getPostByUser.UserID);
            try
            {
                var posts = _postService.GetPostsByUser(getPostByUser.TimelineID, getPostByUser.UserID);
                return Ok(posts);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Post couldn't be retrieved by the user");
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("CreatePost/{timelineId}")]
        [Authorize]
        public IActionResult CreatePost([FromBody] PostPostDTO newPost, [FromRoute] int timelineId)
        {
            _logger.LogInformation("Create the post with the values " + newPost + "in the timeline with id" + timelineId);
            try
            {
                _postService.CreatePost(timelineId, newPost);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Post couldn't be created");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
