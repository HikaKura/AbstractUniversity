using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.BindingModel
{
    [DataContract]
    public class CourseBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string StartCourse { get; set; }
        [DataMember]
        public string EndCourse { get; set; }
        [DataMember]
        public int Student_Count { get; set; }
        [DataMember]
        public int StudyId { get; set; }
        [DataMember]
        public int ClassroomId { get; set; }
        public virtual List<ClassroomCourseBindingModel> ClassroomCourses { get; set; }
    }
}
