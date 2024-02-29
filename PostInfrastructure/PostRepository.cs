using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostInfrastructure
{
    public class PostRepostiroy: IPostRepository
    {
        private RepositoryDBContext _context;

        public PostRepostiroy(RepositoryDBContext context) 
        {
            _context = context;
        }

        public void CreatePost(int timelineId, Post newPost)
        {
            newPost.TimelineID = timelineId;
            _context.Posts.Add(newPost);
            _context.SaveChanges();
        }

        public void DeletePost(int timelineId, int postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.TimelineID == timelineId && p.PostID == postId);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
        }

        public List<Post> GetAllPosts(int timelineId)
        {
            return _context.Posts.Where(p => p.TimelineID == timelineId).ToList();
        }

        public Post GetPost(int timelineID, int postId)
        {


            var post = _context.Posts.FirstOrDefault(p => p.TimelineID == timelineID && p.PostID == postId);
            if (post == null)
            {
                throw new ArgumentException("Post not found.", nameof(postId));
            }
            return post;
        }

        public List<Post> GetPostsByUser(int timelineId, int userId)
        {
            return _context.Posts.Where(p => p.TimelineID == timelineId && p.UserID == userId).ToList();
        }

        public void UpdatePost(int timelineId, int postId, string newText, DateTime? newPostDate = null)
        {
            var post = _context.Posts.FirstOrDefault(p => p.TimelineID == timelineId && p.PostID == postId);
            if (post != null)
            {
                post.Text = newText;
                if (newPostDate.HasValue)
                {
                    post.PostDate = newPostDate.Value;
                }
                _context.SaveChanges();
            }
        }
    }
}

