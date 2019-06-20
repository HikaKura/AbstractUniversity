using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversity
{
    [DataContract]
    public class Request
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int TeacherId { get; set; }
        [DataMember]
        public int CourseId { get; set; }
        [DataMember]
        public virtual Teacher Teacher { get; set; }
        [DataMember]
        public virtual Course Course { get; set; }
    }
}
