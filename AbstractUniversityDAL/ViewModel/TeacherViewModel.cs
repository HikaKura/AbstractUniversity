using AbstractUniversityDAL.BindingModel;
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
    public class TeacherViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Имя преподавателя")]
        public string FirstName { get; set; }
        [DataMember]
        [DisplayName("Фамилия преподавателя")]
        public string LastName { get; set; }
        [DataMember]
        [DisplayName("Отчество преподавателя")]
        public string MiddleName { get; set; }
        [DataMember]
        [DisplayName("Почта преподавателя")]
        public string Mail { get; set; }
        [DataMember]
        [DisplayName("Пароль преподавателя")]
        public string Password { get; set; }
        [DataMember]
        [DisplayName("Кафедра преподавателя")]
        public string Department { get; set; }
        public List<RequestViewModel> Requests { get; set; }
        public List<StudyViewModel> Studies { get; set; }
    }
}
