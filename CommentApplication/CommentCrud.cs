using CommentInfrastructure;
using Domain;
using Domain.DTO_s;
using Messaging;
using Messaging.Messages;

namespace CommentApplication
{
    public class CommentCrud : ICommentCrud
    {
        private readonly ICommentRepository _commentRepository;
        private readonly MessageClient _messageClient;

        public CommentCrud(ICommentRepository commentRepository, MessageClient messageClient)
        {
            _commentRepository = commentRepository;
            _messageClient = messageClient;
        }

        public async void AddComment(CommentDTO commentDTO)
        {
            Comment comment = new Comment
            {
                CommentID = 0,
                UserID = commentDTO.UserID,
                CommentText = commentDTO.CommentText,
                PostID = commentDTO.PostID,
                CommentDate = DateTime.Now
            };
            await _messageClient.Send(new AddIfCommentCreated("Adding comment to post", comment.CommentID, comment.PostID), "AddCommentToPost");

            _commentRepository.AddComment(comment);
        }

        public void DeleteComment(int CommentID)
        {
            _commentRepository.DeleteComment(CommentID);
        }

        public List<Comment> GetComments(int pageSize, int pageNumber, int PostID)
        {
            return _commentRepository.GetComments(pageSize, pageNumber, PostID);
        }

        public void MassDeleteComments(int PostID, int UserID)
        {
            _commentRepository.MassDeleteComments(PostID, UserID);
        }

        public void UpdateComment(Comment comment)
        {
            _commentRepository.UpdateComment(comment);
        }
    }
}
