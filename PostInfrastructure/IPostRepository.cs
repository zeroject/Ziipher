using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostInfrastructure
{
    public interface IPostRepository
    {
        public List<Post> GetAllPosts(int timelineID);
        public void CreatePost(int timelineID, Post newPost);

        public Post GetPost(int timelineID, int postId);

        public void UpdatePost(int timelineID, int postId, string newText, DateTime? newPostDate = null);

        public void DeletePost(int timelineID, int postId);

        public List<Post> GetPostsByUser(int timelineID, int userId);

    }
}
