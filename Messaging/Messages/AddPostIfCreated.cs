using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Messages
{
    public class AddPostIfCreated
    {
        public string Message { get; set; }

        public int PostID { get; set; }

        public int TimelineId { get; set; }

        public AddPostIfCreated (string message, int postID, int timelineId)
        {
            Message = message;
            PostID = postID;
            TimelineId = timelineId;
        }
    }
}
