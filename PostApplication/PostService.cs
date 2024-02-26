using Domain;
using PostInfrastructure;

namespace PostApplication
{
    public class PostService : IPostService
    {
        IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public List<Post> GetAllPosts(int timelineID)
        {
            return _postRepository.GetAllPosts(timelineID);
        }

        public void CreatePost(int timelineID, Post newPost)
        {
            newPost.PostDate = DateTime.Now;
            newPost.TimelineID = timelineID;
            _postRepository.CreatePost(timelineID, newPost);
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
