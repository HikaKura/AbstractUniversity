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
    public class Teacher
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public string FirstName { get; set; }
        [DataMember]
        [Required]
        public string LastName { get; set; }
        [DataMember]
        [Required]
        public string MiddleName { get; set; }
        [DataMember]
        [Required]
        public string Mail { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        [Required]
        public string Department { get; set; }
        [ForeignKey("TeacherId")]
        public virtual List<Study> Studies { get; set; }
        [ForeignKey("TeacherId")]
        public virtual List<Request> Requests { get; set; }
    }
}
