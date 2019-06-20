using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.BindingModel
{
    [DataContract]
    public class ClassroomCourseBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ClassroomId { get; set; }
        [DataMember]
        public int? CourseId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Number { get; set; }
    }
}
