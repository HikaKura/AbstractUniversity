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
    public class ClassromServiceList : IClassroomService
    {
        private DataListSingleton source;

        public ClassromServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ClassroomViewModel> GetList()
        {
            List<ClassroomViewModel> result = source.Classrooms.Select(rec => new ClassroomViewModel
            {
                Id = rec.Id,
                Number = rec.Number,
                Status = rec.Status,
                Pavilion = rec.Pavilion,
                ClassroomCourses = source.ClassroomCourses.Where(recPC => recPC.ClassroomId == rec.Id).Select(recPC => new ClassroomCourseViewModel
                {
                    Id = recPC.Id,
                    ClassroomId = recPC.ClassroomId,
                    CourseId = recPC.CourseId
                }).ToList()
            }).ToList();
            return result;
        }

        public ClassroomViewModel GetElement(int id)
        {
            Classroom element = source.Classrooms.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ClassroomViewModel
                {
                    Id = element.Id,
                    Number = element.Number,
                    Status = element.Status,
                    Pavilion = element.Pavilion,
                    ClassroomCourses = source.ClassroomCourses.Where(recPC => recPC.ClassroomId == element.Id).Select(recPC => new ClassroomCourseViewModel
                    {
                        Id = recPC.Id,
                        ClassroomId = recPC.ClassroomId,
                        CourseId = recPC.CourseId,
                        Name = recPC.Name
                    }).ToList()
                };
            }
            throw new Exception("Аудитория не найдена");
        }

        public void AddElement(ClassroomBindingModel model)
        {
            Classroom element = source.Classrooms.FirstOrDefault(rec => rec.Number == model.Number);
            if (element != null)
            {
                throw new Exception("Уже есть аудитория с таким номером");
            }
            int maxId = source.Classrooms.Count > 0 ? source.Classrooms.Max(rec => rec.Id) : 0;
            source.Classrooms.Add(new Classroom
            {
                Id = maxId + 1,
                Number = model.Number,
                Status = element.Status,
                Pavilion = element.Pavilion,
            });
            // курсы для аудитории
            int maxPCId = source.ClassroomCourses.Count > 0 ? source.ClassroomCourses.Max(rec => rec.Id) : 0;
            // убираем дубли по курсам
            var groupCourses = model.ClassroomCourses.GroupBy(rec => rec.CourseId).Select(rec => new
            {
                CourseId = rec.Key,
                Name = rec.Key
            });
            // добавляем компоненты
            foreach (var groupCourse in groupCourses)
            {
                source.ClassroomCourses.Add(new ClassroomCourse
                {
                    Id = ++maxPCId,
                    ClassroomId = maxId + 1,
                    CourseId = groupCourse.CourseId,
                    Name = groupCourse.Name.ToString()
                });
            }
        }

        public void DelElement(int id)
        {
            Classroom element = source.Classrooms.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по курсам при удалении аудитории
                source.ClassroomCourses.RemoveAll(rec => rec.ClassroomId == id);
                source.Classrooms.Remove(element);
            }
            else
            {
                throw new Exception("Аудитория не найдена");
            }
        }

        public void UpdElement(ClassroomBindingModel model)
        {
            Classroom element = source.Classrooms.FirstOrDefault(rec => rec.Number == model.Number && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть аудитория с таким номером");
            }
            element = source.Classrooms.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Аудитория не найдена");
            }
            element.Number = model.Number;
            element.Status = model.Status;
            element.Pavilion = model.Pavilion;
            int maxPCId = source.ClassroomCourses.Count > 0 ? source.ClassroomCourses.Max(rec => rec.Id) : 0;
            // обновляем существуюущие курсы
            var compIds = model.ClassroomCourses.Select(rec => rec.CourseId).Distinct();
            var updateCourses = source.ClassroomCourses.Where(rec => rec.ClassroomId == model.Id && compIds.Contains(rec.CourseId));
            foreach (var updateCourse in updateCourses)
            {
                updateCourse.Name = model.ClassroomCourses.FirstOrDefault(rec => rec.Id == updateCourse.Id).Name;
            }
            source.ClassroomCourses.RemoveAll(rec => rec.ClassroomId == model.Id && !compIds.Contains(rec.CourseId));
            // новые записи
            var groupCourses = model.ClassroomCourses.Where(rec => rec.Id == 0).GroupBy(rec => rec.CourseId).Select(rec => new
            {
                CourseId = rec.Key,
                Name = rec.Key
            });
            foreach (var groupCourse in groupCourses)
            {
                ClassroomCourse elementPC = source.ClassroomCourses.FirstOrDefault(rec => rec.ClassroomId == model.Id && rec.CourseId == groupCourse.CourseId);
                if (elementPC != null)
                {
                    elementPC.Name += groupCourse.Name;
                }
                else
                {
                    source.ClassroomCourses.Add(new ClassroomCourse
                    {
                        Id = ++maxPCId,
                        ClassroomId = model.Id,
                        CourseId = groupCourse.CourseId,
                        Name = groupCourse.Name.ToString()
                    });
                }
            }
        }
    }
}
