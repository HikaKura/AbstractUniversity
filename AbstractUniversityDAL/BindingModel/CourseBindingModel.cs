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
        public string StartCourse { get; set; }
        public string EndCourse { get; set; }
        public int Student_Count { get; set; }
        public int? StudyId { get; set; }
    }
}
