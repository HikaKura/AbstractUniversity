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
    public class Study
    {
        [DataMember]
        public int Id{ get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        public string Orientation { get; set; }
        [DataMember]
        public int TeacherId { get; set; }
        [DataMember]
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("StudyId")]
        public virtual List<Course> Courses { get; set; }
    }
}
