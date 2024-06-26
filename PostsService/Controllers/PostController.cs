﻿using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostApplication;
using PostApplication.DTO_s;
using PostApplication.Helper;

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
        public ActionResult GetAllPosts( int timelineId)
        {
            _logger.LogInformation("Getting posts for " + timelineId);
            var posts = _postService.GetAllPosts(timelineId);
            return Ok(posts);
        }


        [HttpGet]
        [Route("GetPost")]
        public ActionResult GetPost([FromBody] GetPostDTO getPost)
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
        [Route("CreatePost")]
        public IActionResult CreatePost([FromBody] PostPostDTO newPost)
        {
            _logger.LogInformation("Create the post with the values " + newPost + "in the timeline with id" + newPost.TimelineID);
            try
            {
                _postService.CreatePost(newPost);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Post couldn't be created");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddCommentTopost")]
        public IActionResult AddCommentToPost([FromBody] PostAddComment addComment)
        {
            _logger.LogInformation("Add comment to post with id " + addComment.CommentID );
            try
            {
                _postService.AddCommentToPost(addComment);
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Comment couldn't be added to the post");
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost]
        [Route("AddLikeToPost")]
        public IActionResult AddLikeToPost([FromBody] PostAddLike addLike)
        {
            _logger.LogInformation("Add like to post with id " + addLike.LikeID);
            try
            {
                _postService.AddLikeToPost(addLike);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Like couldn't be added to the post");
                return StatusCode(500, e.Message);
            }
        }
    }
}
