﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostApplication.DTO_s
{
    public class PostUpdateDTO
    {
        public int PostID { get; set; }
        public string Text { get; set; }

        public int UserID { get; set; }

        public int TimelineID { get; set; }

        public DateTime PostDate { get; set; }
    }
}
