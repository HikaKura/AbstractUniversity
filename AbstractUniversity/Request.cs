﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    public class Request
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Course Course { get; set; }
    }
}
