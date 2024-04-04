using Domain;
using Microsoft.EntityFrameworkCore;
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
        private DbContextOptions<RepositoryDBContext> _options;

        public PostRepostiroy(RepositoryDBContext context) 
        {
            _options = new DbContextOptionsBuilder<RepositoryDBContext>().UseInMemoryDatabase("PostDB").Options;
        }

        public void CreatePost(int timelineId, Post newPost)
        {
            using(var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {                
                newPost.TimelineID = timelineId;
                context.Posts.Add(newPost);
                context.SaveChanges();
            }

        }

        public void DeletePost(int timelineId, int postId)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {

                var post = context.Posts.FirstOrDefault(p => p.TimelineID == timelineId && p.PostID == postId);
                if (post != null)
                {
                    context.Posts.Remove(post);
                    context.SaveChanges();
                }
            }
        }

        public async Task<Dictionary<Post, Like>> GetAllPosts(int timelineId)
        {
            using(var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                List<Post> posts = context.Posts.Where(p => p.TimelineID == timelineId).ToList();

                //Get all likes from Http request
                List<Like> likes = GetLikes().Result;

                Dictionary<Post, Like> postLikes = new Dictionary<Post, Like>();
                foreach (var post in posts)
                {
                    Like like = likes.FirstOrDefault(l => l.PostID == post.PostID);
                    postLikes.Add(post, like);
                }
                return postLikes;
            }
        }

        public async Task<Dictionary<Post, Like>> GetPost(int timelineID, int postId)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {

                // Get Post from DB
                var post = context.Posts.FirstOrDefault(p => p.TimelineID == timelineID && p.PostID == postId);
                if (post == null)
                {
                    throw new ArgumentException("Post not found.", nameof(postId));
                }

                // Get Like from Http request
                Like like = GetLike(postId).Result;

                // Combine post and like
                Dictionary<Post, Like> postLikes = new Dictionary<Post, Like>
            {
                { post, like }
            };

                return postLikes;
            }

        }

        public List<Post> GetPostsByUser(int timelineId, int userId)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                return context.Posts.Where(p => p.TimelineID == timelineId && p.UserID == userId).ToList();
            }
        }

        public void UpdatePost(int timelineId, int postId, string newText, DateTime? newPostDate = null)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                var post = context.Posts.FirstOrDefault(p => p.TimelineID == timelineId && p.PostID == postId);
                if (post != null)
                {
                    post.Text = newText;
                    if (newPostDate.HasValue)
                    {
                        post.PostDate = newPostDate.Value;
                    }
                    context.SaveChanges();
                }
            }
        }

        public async Task<List<Like>> GetLikes()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://likeservice:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("GetLikes");
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadFromJsonAsync<List<Like>>().Result;
                }
            }
            return null;
        }

        public async Task<Like> GetLike(int postId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://likeservice:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"GetLike/{postId}");
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadFromJsonAsync<Like>().Result;
                }
            }
            return null;
        }
    }    
}


