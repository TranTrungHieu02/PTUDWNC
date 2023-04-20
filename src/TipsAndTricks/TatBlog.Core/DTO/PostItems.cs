﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.DTO
{
    public class PostItems
    {
        public string CategoryName { get; set; }
        public string Tags { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int PostCount { get; set; }
    }
}