using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.ViewModel
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название курса")]
        public string Name { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Краткое описание курса")]
        public string Content { get; set; }
        [DisplayName("Кто преподает")]
        public string TeacherFIO { get; set; }
        [DisplayName("Количество мест для студентов")]
        public int Student_Count { get; set; }
        [DisplayName("Название обучения")]
        public string StudyName { get; set; }
        [DisplayName("Номер аудитории")]
        public int Number { get; set; }
        [DisplayName("Дата начала курса")]
        public string StartCourse { get; set; }
        [DisplayName("Дата окончания курса")]
        public string EndCourse { get; set; }
        public int StudyId { get; set; }
        public int ClassroomId { get; set; }
        public virtual List<ClassroomCourseViewModel> ClassroomCourses { get; set; }
    }
}
