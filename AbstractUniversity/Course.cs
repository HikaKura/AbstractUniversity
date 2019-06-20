using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime StartCourse { get; set; }
        public DateTime? EndCourse { get; set; }
        public string Content { get; set; }
        public int Student_Count { get; set; }
        public int StudyId { get; set; }
        public CourseStatus Status { get; set; }
        public int ClassroomId { get; set; }
        //public virtual ClassroomCourse ClassroomCourse { get; set; }
        [ForeignKey("CourseId")]
        public virtual List<ClassroomCourse> ClassroomCourses { get; set; }
        [ForeignKey("CourseId")]
        public virtual List<Request> Requests { get; set; }
        public virtual Study Study { get; set; }
    }
}
