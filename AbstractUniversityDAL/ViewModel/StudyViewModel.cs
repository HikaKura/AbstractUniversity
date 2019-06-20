using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.ViewModel
{
    public class StudyViewModel
    {
        public int Id { get; set; }
        [DisplayName("Наименование обучения")]
        public string Name { get; set; }
        [DisplayName("Направление обучения")]
        public string Orientation { get; set; }
        [DisplayName("Фамилия преподавателя")]
        public string TeacherLastName { get; set; }
        public int TeacherId { get; set; }
        public List<CourseViewModel> Course { get; set; }
    }
}
