using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.BindingModel
{
    public class TeacherBindingModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public List<RequestBindingModel> Requests { get; set; }
    }
}
