using Domain;

namespace CommentInfrastructure
{
    public interface ICommentRepository
    {
        public List<Comment> GetComments(int pageSize, int pageNumber, int PostID);
        public void AddComment(int PostID, string Comment);
        public void UpdateComment(int CommentID, string Comment);
        public void DeleteComment(int CommentID);
    }
}
