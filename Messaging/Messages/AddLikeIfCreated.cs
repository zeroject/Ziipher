using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Messages
{
    public class AddLikeIfCreated
    {
        public string Message { get; set; }

        public int PostID { get; set; }

        public int LikeID { get; set; }
        public AddLikeIfCreated (string message, int postID, int likeID)
        {
            Message = message;
            PostID = postID;
            LikeID = likeID;
        }
    }
}
