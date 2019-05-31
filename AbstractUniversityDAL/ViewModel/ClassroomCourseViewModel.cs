using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.ViewModel
{
    public class ClassroomCourseViewModel
    {
        public int Id { get; set; }
        public int ClassroomId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
    }
}
