using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractUniversity
{
    [DataContract]
    public class Classroom
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public int Number { get; set; }
        [DataMember]
        [Required]
        public bool Status { get; set; }
        [DataMember]
        [Required]
        public int Pavilion { get; set; }
        [ForeignKey("ClassroomId")]
        public virtual List<ClassroomCourse> ClassroomCourses { get; set; }
    }
}
