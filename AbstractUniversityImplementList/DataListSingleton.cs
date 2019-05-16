using AbstractUniversity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Classroom> Classrooms { get; set; }
        public List<Course> Courses { get; set; }
        public List<ClassroomCourse> ClassroomCourses { get; set; }
        public List<Request> Requests { get; set; }
        public List<Study> Studies { get; set; }
        public List<Teacher> Teachers { get; set; }

        private DataListSingleton()
        {
            Classrooms = new List<Classroom>();
            Courses = new List<Course>();
            ClassroomCourses = new List<ClassroomCourse>();
            Requests = new List<Request>();
            Studies = new List<Study>();
            Teachers = new List<Teacher>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
