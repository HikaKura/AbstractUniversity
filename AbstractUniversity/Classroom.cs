using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    public class Classroom
    {
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int Pavilion { get; set; }
        [ForeignKey("ClassroomId")]
        public virtual List<ClassroomCourse> ClassroomCourses { get; set; }
    }
}
