﻿using Domain;
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
        public IActionResult GetPost([FromRoute] int timelineId, [FromRoute] int postId)
        {
            _logger.LogInformation("Get the post with ID " + postId + "from timeline with id " + timelineId);
            var post = _postService.GetPost(timelineId, postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpDelete]
        [Route("DeletePost/{timelineId}/{postId}")]
        public IActionResult DeletePost([FromRoute] int timelineId, [FromRoute] int postId)
        {
            _logger.LogInformation("Delete post with id " + postId + "from timeline with id " + timelineId);
            _postService.DeletePost(timelineId, postId);
            return Ok();
        }

        [HttpPut]

        [Route("UpdatePost/{timelineId}/{postId}")]
        public IActionResult UpdatePost([FromRoute] int timelineId, [FromRoute] int postId, [FromBody] Post postUpdate)
        {
            try
            {
                _logger.LogInformation("Update post with id " + postId + "from timeline with id" + timelineId + "with the updated post " + postUpdate);
                _postService.UpdatePost(timelineId, postId, postUpdate.Text, postUpdate.PostDate);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("GetPostsByUser/{timelineId}/{userId}")]
        public IActionResult GetPostsByUser([FromRoute] int timelineId, [FromRoute] int userId)
        {
            _logger.LogInformation("Get the posts from timeline with the id " + timelineId + "from the user with id" + userId);
            var posts = _postService.GetPostsByUser(timelineId, userId);
            if (posts == null || !posts.Any())
            {
                return NotFound();
            }
            return Ok(posts);
        }

        [HttpPost]
        [Route("CreatePost/{timelineId}")]
        public IActionResult CreatePost([FromBody] PostPostDTO newPost, [FromRoute] int timelineId)
        {
            _logger.LogInformation("Create the post with the values " + newPost + "in the timeline with id" + timelineId);
            _postService.CreatePost(timelineId, newPost);
            return Ok();
        }
    }
}
