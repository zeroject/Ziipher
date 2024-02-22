using CommentInfrastructure;
using Domain;

namespace CommentApplication
{
    public class CommentCrud : ICommentCrud
    {
        private readonly ICommentRepository _commentRepository;

        public CommentCrud(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void AddComment(int PostID, string Comment)
        {
            _commentRepository.AddComment(PostID, Comment);
        }

        public void DeleteComment(int CommentID)
        {
            _commentRepository.DeleteComment(CommentID);
        }

        public List<Comment> GetComments(int pageSize, int pageNumber, int PostID)
        {
            return _commentRepository.GetComments(pageSize, pageNumber, PostID);
        }

        public void UpdateComment(int CommentID, string Comment)
        {
            _commentRepository.UpdateComment(CommentID, Comment);
        }
    }
}
