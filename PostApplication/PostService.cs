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

        public List<Post> GetAllPosts(int timelineID)
        {
            return _postRepository.GetAllPosts(timelineID);
        }

        public async Task<Post> CreatePost(PostPostDTO newPost)
        {
            newPost.PostDate = DateTime.Now;
            var post = await _postRepository.CreatePost(_mapper.Map<Post>(newPost));
            await _messageClient.Send(new AddPostIfCreated("Adding post to timeline", post.PostID, newPost.TimelineID), "AddPostToTimeline");
            return post;
        }

        public Post GetPost(int timelineID, int postId)
        {
            return _postRepository.GetPost(timelineID, postId);
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
