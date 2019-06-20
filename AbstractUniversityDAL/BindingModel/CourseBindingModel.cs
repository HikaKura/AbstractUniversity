using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.BindingModel
{
    public class CourseBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int Student_Count { get; set; }
        public int StudyId { get; set; }
        public int ClassroomId { get; set; }
        public virtual List<ClassroomCourseBindingModel> ClassroomCourses { get; set; }
    }
}
