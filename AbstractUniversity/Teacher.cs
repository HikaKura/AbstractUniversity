using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Department { get; set; }
        [ForeignKey("TeacherId")]
        public virtual List<Study> Studies { get; set; }
        [ForeignKey("TeacherId")]
        public virtual List<Request> Requests { get; set; }
    }
}
