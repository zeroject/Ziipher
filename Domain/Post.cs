﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Post
    {
        public int PostID { get; set; }
        public required string Text { get; set; }
        public DateTime PostDate { get; set; }
        public List<int> Comments { get; set; } = new List<int>();
        public int UserID { get; set; } 
        public int TimelineID { get; set; }

        public List<int> Likes { get; set; } = new List<int>();
    }
}
