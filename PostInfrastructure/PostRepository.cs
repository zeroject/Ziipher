using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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

        public async Task<Dictionary<Post, Like>> GetAllPosts(int timelineId)
        {
            //Get all posts from DB
            List<Post> posts = _context.Posts.Where(p => p.TimelineID == timelineId).ToList();

            //Get all likes from Http request
            List<Like> likes = new List<Like>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://likeservice:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("GetLikes");
                if (response.IsSuccessStatusCode)
                {
                    likes = response.Content.ReadFromJsonAsync<List<Like>>().Result;
                }
            }

            //Combine posts and likes
            Dictionary<Post, Like> postLikes = new Dictionary<Post, Like>();
            foreach (var post in posts)
            {
                Like like = likes.FirstOrDefault(l => l.PostID == post.PostID);
                postLikes.Add(post, like);
            }
            return postLikes;
        }

        public async Task<Dictionary<Post, Like>> GetPost(int timelineID, int postId)
        {
            // Get Post from DB
            var post = _context.Posts.FirstOrDefault(p => p.TimelineID == timelineID && p.PostID == postId);
            if (post == null)
            {
                throw new ArgumentException("Post not found.", nameof(postId));
            }

            // Get Like from Http request
            Like like = new Like();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://likeservice:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("GetLike/" + postId);
                if (response.IsSuccessStatusCode)
                {
                    like = response.Content.ReadFromJsonAsync<Like>().Result;
                }
            }

            // Combine post and like
            Dictionary<Post, Like> postLikes = new Dictionary<Post, Like>
            {
                { post, like }
            };

            return postLikes;
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

