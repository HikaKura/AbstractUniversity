using AbstractUniversityDAL.BindingModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.ViewModel
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя преподавателя")]
        public string FirstName { get; set; }
        [DisplayName("Фамилия преподавателя")]
        public string LastName { get; set; }
        [DisplayName("Отчество преподавателя")]
        public string MiddleName { get; set; }
        [DisplayName("Почта преподавателя")]
        public string Mail { get; set; }
        [DisplayName("Пароль преподавателя")]
        public string Password { get; set; }
        [DisplayName("Кафедра преподавателя")]
        public string Department { get; set; }
        public virtual List<RequestViewModel> Requests { get; set; }
        public virtual List<StudyViewModel> Studies { get; set; }
    }
}
