using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.ViewModel
{
    [DataContract]
    public class ClassroomCourseViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ClassroomId { get; set; }
        [DataMember]
        public int? CourseId { get; set; }
        [DataMember]
        [DisplayName("Название курса")]
        public string Name { get; set; }
        [DataMember]
        [DisplayName("Номер кабинета")]
        public int Number { get; set; }
    }
}
