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
        public IActionResult GetAllPosts( int timelineId)
        {
            _logger.LogInformation("Getting posts for " + timelineId);
            var posts = _postService.GetAllPosts(timelineId);
            return Ok(posts);
        }

        [HttpGet]
        [Route("GetPost/{timelineId}/{postID}")]
        public async Task<IActionResult> GetPost([FromRoute] int timelineId, [FromRoute] int postId)
        {
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
            try
            {
                _logger.LogInformation("Get the post with ID " + postId + "from timeline with id " + timelineId);
                var post = _postService.GetPost(timelineId, postId);             
                return Ok(post);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Post couldn't be retrieved");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePost/{timelineId}/{postId}")]
        [Authorize]
        public async Task<IActionResult> DeletePost([FromRoute] int timelineId, [FromRoute] int postId)
        {
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
            try
            {
                _logger.LogInformation("Delete post with id " + postId + "from timeline with id " + timelineId);
                _postService.DeletePost(timelineId, postId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post couldn't be deleted");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePost/{timelineId}/{postId}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost([FromRoute] int timelineId, [FromRoute] int postId, [FromBody] Post postUpdate)
        {
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
            try
            {
                _logger.LogInformation("Update post with id " + postId + "from timeline with id" + timelineId + "with the updated post " + postUpdate);
                _postService.UpdatePost(timelineId, postId, postUpdate.Text, postUpdate.PostDate);
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
        public async Task<IActionResult> GetPostsByUser([FromRoute] int timelineId, [FromRoute] int userId)
        {
            _logger.LogInformation("Get the posts from timeline with the id " + timelineId + "from the user with id" + userId);
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
            try
            {
                var posts = _postService.GetPostsByUser(timelineId, userId);
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
        public async Task<IActionResult> CreatePost([FromBody] PostPostDTO newPost, [FromRoute] int timelineId)
        {
            _logger.LogInformation("Create the post with the values " + newPost + "in the timeline with id" + timelineId);
            string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool v = await ValidateTokenAsync(accessToken);
            if (v != true)
                return Unauthorized();
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

        private async Task<bool> ValidateTokenAsync(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                _logger.LogError("Authorization token not found in request headers");
                return false;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://authservice:8080");
                var response = await client.PostAsJsonAsync($"/auth/validateUser?token={accessToken}", "");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error validating user to AuthWanabe");
                    return false;
                }
                var data = await response.Content.ReadFromJsonAsync<bool>();
                if (data != true)
                {
                    _logger.LogInformation("User failed to auth himself");
                    return false;
                }
                return true;
            }
        }
    }
}
