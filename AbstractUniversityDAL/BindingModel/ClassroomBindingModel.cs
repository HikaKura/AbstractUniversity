using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.BindingModel
{
    [DataContract]
    public class ClassroomBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public int Pavilion { get; set; }
        public virtual List<ClassroomCourseBindingModel> ClassroomCourses { get; set; }
    }
}
