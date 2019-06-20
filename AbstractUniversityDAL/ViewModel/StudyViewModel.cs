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
    public class StudyViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Наименование обучения")]
        public string Name { get; set; }
        [DataMember]
        [DisplayName("Направление обучения")]
        public string Orientation { get; set; }
        [DataMember]
        [DisplayName("Фамилия преподавателя")]
        public string TeacherLastName { get; set; }
        [DataMember]
        public int TeacherId { get; set; }
        public List<CourseViewModel> Course { get; set; }
    }
}
