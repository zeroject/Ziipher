using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Timeline
    {
        public int TimelineID { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();

        public int UserID { get; set; }

    }
}
