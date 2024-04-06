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
        public List<int>? PostIDs { get; set; }
        public int UserID { get; set; }
    }
}
