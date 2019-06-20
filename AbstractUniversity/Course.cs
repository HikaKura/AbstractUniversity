using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    [DataContract]
    public class Course
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        public DateTime StartCourse { get; set; }
        [DataMember]
        public DateTime? EndCourse { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public int Student_Count { get; set; }
        [DataMember]
        public int StudyId { get; set; }
        [DataMember]
        public CourseStatus Status { get; set; }
        [DataMember]
        public int ClassroomId { get; set; }
        //public virtual ClassroomCourse ClassroomCourse { get; set; }
        [ForeignKey("CourseId")]
        public virtual List<ClassroomCourse> ClassroomCourses { get; set; }
        [ForeignKey("CourseId")]
        public virtual List<Request> Requests { get; set; }
        [DataMember]
        public virtual Study Study { get; set; }
    }
}
