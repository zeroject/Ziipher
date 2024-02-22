using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentInfrastructure
{
    public class CommentRepository : ICommentRepository
    {
        public void AddComment(int PostID, string Comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(int CommentID)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetComments(int pageSize, int pageNumber, int PostID)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(int CommentID, string Comment)
        {
            throw new NotImplementedException();
        }
    }
}
