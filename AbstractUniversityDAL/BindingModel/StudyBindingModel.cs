using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.BindingModel
{
    [DataContract]
    public class StudyBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Orientation { get; set; }
        [DataMember]
        public int TeacherId { get; set; }
        public List<CourseBindingModel> Courses { get; set; }
    }
}
