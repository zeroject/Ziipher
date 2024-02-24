using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CommentInfrastructure
{
    public class CommentRepository : ICommentRepository
    {
        private DbContextOptions<DbContext> _options;
        public CommentRepository()
        {
            _options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase("CommentDB").Options;
        }
        public void AddComment(Comment comment)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                _ = context.Add(comment);
                context.SaveChanges();
            }
        }

        public void DeleteComment(int CommentID)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                Comment commentToUpdate = context.Comments.Find(CommentID);
                _ = context.Comments.Remove(commentToUpdate);
                context.SaveChanges();
            }
        }

        public List<Comment> GetComments(int pageSize, int pageNumber, int PostID)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                return context.Comments.Where(c => c.PostID == PostID).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            }
        }

        public void MassDeleteComments(int PostID, int UserID)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                context.Comments.RemoveRange(context.Comments.Where(c => c.PostID == PostID));
                context.SaveChanges();
            }
        }

        public void UpdateComment(Comment comment)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                Comment commentToUpdate = context.Comments.Find(comment.CommentID);
                commentToUpdate.CommentText = comment.CommentText;
                commentToUpdate.CommentDate = DateTime.Now;
                _ = context.Comments.Update(commentToUpdate);
                context.SaveChanges();
            }
        }
    }
}
