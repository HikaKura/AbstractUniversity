using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public virtual List<Study> Studies { get; set; }
        public virtual List<Request> Requests { get; set; }
    }
}
