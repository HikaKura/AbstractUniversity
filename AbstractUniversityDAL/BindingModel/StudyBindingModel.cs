using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.BindingModel
{
    public class StudyBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Orientation { get; set; }
        public int TeacherId { get; set; }
        public List<CourseBindingModel> Courses { get; set; }
    }
}
