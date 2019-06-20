using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.BindingModel
{
    [DataContract]
    public class RequestBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int TeacherId { get; set; }
        [DataMember]
        public int CourseId { get; set; }
    }
}
