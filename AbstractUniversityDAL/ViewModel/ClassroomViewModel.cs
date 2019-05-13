using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.ViewModel
{
    public class ClassroomViewModel
    {
        public int Id { get; set; }
        [DisplayName("Номер кабинета")]
        public int Number { get; set; }
        [DisplayName("Занятость")]
        public bool Status { get; set; }
        [DisplayName("Номер корпуса")]
        public int Pavilion { get; set; }
        public virtual List<ClassroomCourseViewModel> ClassroomCourses { get; set; }
    }
}
