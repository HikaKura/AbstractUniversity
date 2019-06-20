using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.BindingModel
{
    [DataContract]
    public class TeacherBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Department { get; set; }
        public List<RequestBindingModel> Requests { get; set; }
        public List<StudyBindingModel> Studies { get; set; }
    }
}
