using Domain;
using Domain.DTO_s;
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
        public void AddComment(CommentDTO commentDTO);
        public void UpdateComment(Comment comment);
        public void DeleteComment(int CommentID);
        public void MassDeleteComments(int PostID, int UserID);
    }
}
