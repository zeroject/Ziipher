using AutoMapper;
using Domain;
using Messaging;
using Messaging.Messages;
using PostApplication.DTO_s;
using PostInfrastructure;

namespace PostApplication
{
    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IMapper _mapper;
        private MessageClient _messageClient;

        public PostService(IPostRepository postRepository, IMapper mapper, MessageClient messageClient)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _messageClient = messageClient;
        }

        public Dictionary<Post, Like> GetAllPosts(int timelineID)
        {
            return _postRepository.GetAllPosts(timelineID).Result;
        }

        public async Task<Post> CreatePost(int timelineID, PostPostDTO newPost)
        {
            newPost.PostDate = DateTime.Now;
            newPost.TimelineID = timelineID;
            var post = await _postRepository.CreatePost(timelineID, _mapper.Map<Post>(newPost));
            await _messageClient.Send(new AddPostIfCreated("Adding post to timeline", post.PostID, timelineID), "AddPostToTimeline");
            return post;
        }

        public Dictionary<Post, Like> GetPost(int timelineID, int postId)
        {
            return _postRepository.GetPost(timelineID, postId).Result;
        }

        public void UpdatePost(int timelineID, int postId, string newText, DateTime? newPostDate = null)
        {
           _postRepository.UpdatePost(timelineID, postId, newText, newPostDate);
        }

        public void DeletePost(int timelineID, int postId)
        {
            _postRepository.DeletePost(timelineID, postId);
        }

        public List<Post> GetPostsByUser(int timelineID, int userId)
        {
            return _postRepository.GetPostsByUser(timelineID, userId);
        }

    }
}
