using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    public class ClassroomCourse
    {
        public int Id { get; set; }
        public int ClassroomId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public virtual Classroom Classroom { get; set; }
        public virtual Course Course { get; set; }
    }
}
