using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    public class Study
    {
        public int Id{ get; set; }
        [Required]
        public string Name { get; set; }
        public string Orientation { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("StudyId")]
        public virtual List<Course> Courses { get; set; }
    }
}
