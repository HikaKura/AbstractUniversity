using AbstractUniversity;
using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityImplementList.Implementations
{
    public class StudyServiceDB
    {
        private DataListSingleton source;
        public StudyServiceDB()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<StudyViewModel> GetList()
        {
            List<StudyViewModel> result = source.Studies.Select(rec => new StudyViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Orientation = rec.Orientation,
                TeacherId = rec.TeacherId,
                Course = source.Courses.Where(recPC => recPC.Id == rec.Id).Select(recPC => new CourseViewModel
                {
                    Id = recPC.Id,
                    Name = recPC.Name,
                    StartCourse = recPC.StartCourse,
                    EndCourse = recPC.EndCourse,
                    Content = recPC.Content,
                    StudyId = recPC.StudyId,
                    Student_Count = recPC.Student_Count
                }).ToList()
            }).ToList();
            return result;
        }
        public StudyViewModel GetElement(int id)
        {
            Study element = source.Studies.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StudyViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    Orientation = element.Orientation,
                    TeacherId = element.TeacherId,
                    Course = source.Courses.Where(recPC => recPC.Id == element.Id).Select(recPC => new CourseViewModel
                     {
                         Id = recPC.Id,
                         Name = recPC.Name,
                         StartCourse = recPC.StartCourse,
                         EndCourse = recPC.EndCourse,
                         Content = recPC.Content,
                         StudyId = recPC.StudyId,
                         Student_Count = recPC.Student_Count
                     }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(StudyBindingModel model)
        {
            Study element = source.Studies.FirstOrDefault(rec => rec.Name == model.Name);
            if (element != null)
            {
                throw new Exception("Уже есть бучение с таким именем.");
            }
            int maxId = source.Studies.Count > 0 ? source.Studies.Max(rec => rec.Id) : 0;
            source.Studies.Add(new Study
            {
                Id = element.Id,
                Name = element.Name,
                Orientation = element.Orientation,
                TeacherId = element.TeacherId
            });
            // курсы для аудитории
            int maxPCId = source.Courses.Count > 0 ? source.Courses.Max(rec => rec.Id) : 0;
            // убираем дубли по курсам
            var groupCourses = model.Courses.GroupBy(rec => rec.Id).Select(rec => new
            {
                CourseId = rec.Key,
                Name = rec.Where(recPC => recPC.Id == rec.Key).Select(r => r.Name)
            });
            // добавляем компоненты
            foreach (var groupCourse in groupCourses)
            {
                source.Courses.Add(new Course
                {
                    Id = ++maxPCId,
                    StudyId = model.Id,
                    Name = groupCourse.Name.ToString(),
                });
            }
        }
        public void UpdElement(StudyBindingModel model)
        {
            Study element = source.Studies.FirstOrDefault(rec => rec.Name == model.Name && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть обучение с таким именем");
            }
            element = source.Studies.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Name = model.Name;
            element.Orientation = model.Orientation;
            element.TeacherId = model.TeacherId;
            
        }
        public void DelElement(int id)
        {
            Study element = source.Studies.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Studies.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
