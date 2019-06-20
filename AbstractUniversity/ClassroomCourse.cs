using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    [DataContract]
    public class ClassroomCourse
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
        [DataMember]
        public virtual Classroom Classroom { get; set; }
        [DataMember]
        public virtual Course Course { get; set; }
    }
}
