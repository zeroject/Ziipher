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

        public PostRepostiroy() 
        {
            _options = new DbContextOptionsBuilder<RepositoryDBContext>().UseInMemoryDatabase("PostDB").Options;
        }

        public async Task<Post> CreatePost( Post newPost)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                context.Posts.Add(newPost);
                context.SaveChanges();
                return newPost;
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

        public List<Post> GetAllPosts(int timelineId)
        {
            using(var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                List<Post> posts = context.Posts.Where(p => p.TimelineID == timelineId).ToList();

                return posts;
            }
        }

        public Post GetPost(int timelineID, int postId)
        {
            using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                // Get Post from DB
                var post = context.Posts.FirstOrDefault(p => p.TimelineID == timelineID && p.PostID == postId);
                if (post == null)
                {
                    throw new ArgumentException("Post not found.", nameof(postId));
                }
                return post;
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

        public async Task AddCommentToPost(Comment comment)
        {
                using (var context = new RepositoryDBContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
                {
                    var post = context.Posts.Find(comment.PostID);
                    if (post == null)
                    {
                        throw new ArgumentException("Post not found.", nameof(comment.PostID));
                    }
                    post.Comments.Add(comment.CommentID);
                    await context.SaveChangesAsync();
            }
        }
        }
    }


