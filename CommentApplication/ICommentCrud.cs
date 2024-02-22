using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentApplication
{
    public interface ICommentCrud
    {
        public List<Comment> GetComments(int pageSize, int pageNumber, int PostID);
        public void AddComment(int PostID, string Comment);
        public void UpdateComment(int CommentID, string Comment);
        public void DeleteComment(int CommentID);
    }
}
