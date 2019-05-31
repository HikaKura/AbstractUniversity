using AbstractUniversity;
using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.Interface;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityImplementList.Implementations
{
    public class CourseServiceDB : ICourseService
    {
        private DataListSingleton source;

        public List<CourseViewModel> GetList()
        {
            List<CourseViewModel> result = source.Courses.Select(rec => new CourseViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                StartCourse = rec.StartCourse,
                EndCourse = rec.EndCourse,
                Content = rec.Content,
                Student_Count = rec.Student_Count,
                StudyId = rec.StudyId
            }).ToList();
            return result;
        }

        public CourseViewModel GetElement(int id)
        {
            Course element = source.Courses.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CourseViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    StartCourse = element.StartCourse,
                    EndCourse = element.EndCourse,
                    Content = element.Content,
                    Student_Count = element.Student_Count,
                    StudyId = element.StudyId
                };
            }
            throw new Exception("Курс не найден");
        }

        public void AddElement(CourseBindingModel model)
        {
            Course element = source.Courses.FirstOrDefault(rec => rec.Name == model.Name);
            if (element != null)
            {
                throw new Exception("Уже есть курс с таким названием");
            }
            int maxId = source.Courses.Count > 0 ? source.Courses.Max(rec => rec.Id) : 0;
            source.Courses.Add(new Course
            {
                Id = maxId + 1,
                Name = model.Name,
                StartCourse = model.StartCourse,
                EndCourse = model.EndCourse,
                Content = model.Content,
                Student_Count = model.Student_Count,
                StudyId = model.StudyId
            });
        }

        public void UpdElement(CourseBindingModel model)
        {
            Course element = source.Courses.FirstOrDefault(rec => rec.Name == model.Name && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть курс с таким названием");
            }
            element = source.Courses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Name = model.Name;
        }

        public void DelElement(int id)
        {
            Course element = source.Courses.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Courses.RemoveAll(rec => rec.Id == id);
            }
            else
            {
                throw new Exception("Курс не найден");
            }
        }
    }
}
