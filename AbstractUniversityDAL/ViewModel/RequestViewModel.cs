using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.ViewModel
{
    [DataContract]
    public class RequestViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int TeacherId { get; set; }
        [DataMember]
        public int CourseId { get; set; }
    }
}
