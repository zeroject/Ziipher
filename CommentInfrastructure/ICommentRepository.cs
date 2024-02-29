using Domain;

namespace CommentInfrastructure
{
    public interface ICommentRepository
    {
        public List<Comment> GetComments(int pageSize, int pageNumber, int PostID);
        public void AddComment(Comment comment);
        public void UpdateComment(Comment comment);
        public void DeleteComment(int CommentID);
        public void MassDeleteComments(int PostID, int UserID);
    }
}
