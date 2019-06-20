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
    public class ClassroomViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Номер кабинета")]
        public int Number { get; set; }
        [DataMember]
        [DisplayName("Свободно?")]
        public bool Status { get; set; }
        [DataMember]
        [DisplayName("Номер корпуса")]
        public int Pavilion { get; set; }
        public virtual List<ClassroomCourseViewModel> ClassroomCourses { get; set; }
    }
}
