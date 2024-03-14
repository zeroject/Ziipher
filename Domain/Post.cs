using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Post
    {
        public int PostID { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public int UserID { get; set; } 
        public int TimelineID { get; set; }
    }
}
