using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Messages
{
    public class AddIfCommentCreated
    {
        public string Message { get; set; }

        public int CommentID { get; set; }

        public int PostID { get; set; }

        public AddIfCommentCreated(string message, int commentID, int postID)
        {
            Message = message;
            CommentID = commentID;
            PostID = postID;
        }
    }
}
