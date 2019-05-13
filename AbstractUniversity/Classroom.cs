using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    public class Classroom
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool Status { get; set; }
        public int Pavilion { get; set; }
        public virtual List<ClassroomCourse> ClassroomCourses { get; set; }
    }
}
