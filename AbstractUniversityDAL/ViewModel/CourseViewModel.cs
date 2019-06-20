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
    public class CourseViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название курса")]
        public string Name { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DataMember]
        [DisplayName("Краткое описание курса")]
        public string Content { get; set; }
        [DataMember]
        [DisplayName("Кто преподает")]
        public string TeacherFIO { get; set; }
        [DataMember]
        [DisplayName("Количество мест для студентов")]
        public int Student_Count { get; set; }
        [DataMember]
        [DisplayName("Название обучения")]
        public string StudyName { get; set; }
        [DataMember]
        [DisplayName("Номер аудитории")]
        public int Number { get; set; }
        [DataMember]
        [DisplayName("Дата начала курса")]
        public string StartCourse { get; set; }
        [DataMember]
        [DisplayName("Дата окончания курса")]
        public string EndCourse { get; set; }
        [DataMember]
        public int StudyId { get; set; }
        [DataMember]
        public int ClassroomId { get; set; }
        public virtual List<ClassroomCourseViewModel> ClassroomCourses { get; set; }
    }
}
