using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartCourse { get; set; }
        public DateTime EndCourse { get; set; }
        public string Content { get; set; }
        public int Student_Count { get; set; }
        public int StudyId { get; set; }
        public virtual List<ClassroomCourse> ClassroomCourses { get; set; }
        public virtual List<Request> Requests { get; set; }
    }
}
