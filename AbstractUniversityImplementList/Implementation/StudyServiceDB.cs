using AbstractUniversity;
using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.Interface;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityImplementList.Implementation
{
    class StudyServiceDB : IStudyService
    {
        private UniversityDbContext context;

        public StudyServiceDB(UniversityDbContext context)
        {
            this.context = context;
        }

        public void AddElement(StudyBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Study element = context.Studies.FirstOrDefault(rec =>
                        rec.Name == model.Name);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Studies.Add(new Study
                    {
                        Name = model.Name,
                        Orientation = model.Orientation,
                        TeacherId = model.TeacherId,
                        Id = model.Id,
                    });
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupCourses = model.Courses
                        .GroupBy(rec => rec.Id)
                        .Select(rec => new
                        {
                            CourseId = rec.Key
                        });
                    // добавляем компоненты
                    foreach (var groupCourse in groupCourses)
                    {
                        context.Courses.Add(new Course
                        {
                            StudyId = element.Id,
                            Id = groupCourse.CourseId,
                            Name = context.Courses.ElementAt(groupCourse.CourseId).Name
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Study element = context.Studies.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.Courses.RemoveRange(context.Courses.Where(rec =>
                            rec.StudyId == id));
                        context.Studies.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public StudyViewModel GetElement(int id)
        {
            Study element = context.Studies.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StudyViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    Orientation = element.Orientation,
                    TeacherId = element.TeacherId,
                    Course = context.Courses
                        .Where(recPC => recPC.StudyId == element.Id)
                        .Select(recPC => new CourseViewModel
                        {
                            Id = recPC.Id,
                            StudyId = recPC.StudyId,
                            Name = recPC.Name,
                            Content = recPC.Content,
                            StartCourse = recPC.StartCourse,
                            EndCourse = recPC.EndCourse,
                            Student_Count = recPC.Student_Count
                        })
                        .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<StudyViewModel> GetList()
        {
            List<StudyViewModel> result = context.Studies.Select(rec => new StudyViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Orientation = rec.Orientation,
                TeacherId = rec.TeacherId,
                Course = context.Courses
                    .Where(recPC => recPC.StudyId == rec.Id)
                    .Select(recPC => new CourseViewModel
                    {
                        Id = recPC.Id,
                        StudyId = recPC.StudyId,
                        Content = recPC.Content,
                        StartCourse = recPC.StartCourse,
                        EndCourse = recPC.EndCourse,
                        Name = recPC.Name,
                        Student_Count = recPC.Student_Count
                    })
                    .ToList()
            })
            .ToList();
            return result;
        }

        public void UpdElement(StudyBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Study element = context.Studies.FirstOrDefault(rec =>
                        rec.Name == model.Name && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Studies.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.Name = model.Name;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.Courses.Select(rec =>
                        rec.Id).Distinct();
                    var updateCourses = context.Courses.Where(rec =>
                        rec.StudyId == model.Id && compIds.Contains(rec.Id));
                    foreach (var updateCourse in updateCourses)
                    {
                        updateCourse.Name =
                        model.Courses.FirstOrDefault(rec => rec.Id == updateCourse.Id).Name;
                    }
                    context.SaveChanges();
                    context.Courses.RemoveRange(context.Courses.Where(rec =>
                        rec.StudyId == model.Id && !compIds.Contains(rec.Id)));
                    context.SaveChanges();
                    // новые записи
                    var groupCourses = model.Courses
                        .Where(rec => rec.Id == 0)
                        .GroupBy(rec => rec.Id)
                        .Select(rec => new
                        {
                            CourseId = rec.Key
                        });
                    foreach (var groupCourse in groupCourses)
                    {
                        Course elementPC = context.Courses
                            .FirstOrDefault(rec => rec.StudyId == model.Id
                                            && rec.Id == groupCourse.CourseId);
                        if (elementPC != null)
                        {
                            elementPC.Name = context.Courses.ElementAt(groupCourse.CourseId).Name;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.Courses.Add(new Course
                            {
                                StudyId = model.Id,
                                Id = groupCourse.CourseId,
                            });
                        }
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
