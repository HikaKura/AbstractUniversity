using AbstractUniversity;
using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.Interface;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace AbstractUniversityImplementList.Implementation
{
    public class TeacherServiceDB : ITeacherService
    {
        private UniversityDbContext context;

        public TeacherServiceDB(UniversityDbContext context)
        {
            this.context = context;
        }

        public void AddElement(TeacherBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Teacher element = context.Teachers.FirstOrDefault(rec =>
                        rec.FirstName == model.FirstName && rec.MiddleName == model.MiddleName && rec.LastName == model.LastName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть преподаватель с таким именем");
                    }
                    element = context.Teachers.Add(new Teacher
                    {
                        
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Department = model.Department,
                   //     Password = model.Password,
                        Mail = model.Mail
                    });
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    //var groupStudies = model.Studies
                    //    .GroupBy(rec => rec.Id)
                    //    .Select(rec => new
                    //    {
                    //        StudyId = rec.Key
                    //    });
                    //// добавляем компоненты
                    //foreach (var groupStudy in groupStudies)
                    //{
                    //    context.Studies.Add(new Study
                    //    {
                    //        TeacherId = element.Id,
                    //        Id = groupStudy.StudyId
                    //    });
                    //    context.SaveChanges();
                    //}
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
                    Teacher element = context.Teachers.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.Studies.RemoveRange(context.Studies.Where(rec =>
                            rec.TeacherId == id));
                        context.Teachers.Remove(element);
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

        public TeacherViewModel GetElement(int id)
        {
            Teacher element = context.Teachers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new TeacherViewModel
                {
                    Id = element.Id,
                    FirstName = element.FirstName,
                    MiddleName = element.MiddleName,
                    LastName = element.LastName,
                    Department = element.Department,
                    Mail = element.Mail,
                  //  Password = element.Password,
                    Requests = context.Requests.Where(recPC => recPC.TeacherId == element.Id).Select(recPC => new RequestViewModel
                    {
                        Id = recPC.Id,
                        CourseId = recPC.CourseId,
                        TeacherId = recPC.TeacherId
                    }).ToList(),
                    Studies = context.Studies.Where(recPC => recPC.TeacherId == element.Id).Select(recPC => new StudyViewModel
                    {
                        Id = recPC.Id,
                        Name = recPC.Name,
                        Orientation = recPC.Orientation,
                        TeacherId = recPC.TeacherId,
                        Course = context.Courses.Where(r => r.Id == element.Id).Select(r => new CourseViewModel
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Content = r.Content,
                            StartCourse = SqlFunctions.DateName("dd", r.StartCourse) + " " + SqlFunctions.DateName("mm", r.StartCourse) + " " + SqlFunctions.DateName("yyyy", r.StartCourse),
                            EndCourse = r.EndCourse == null ? "" : SqlFunctions.DateName("dd", r.EndCourse) + " " + SqlFunctions.DateName("mm", r.EndCourse) + " " + SqlFunctions.DateName("yyyy", r.EndCourse),
                            Student_Count = r.Student_Count,
                            StudyId = r.StudyId
                        }).ToList(),
                    }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<TeacherViewModel> GetList()
        {
            List<TeacherViewModel> result = context.Teachers.Select(rec => new TeacherViewModel
            {
                Id = rec.Id,
                FirstName = rec.FirstName,
                MiddleName = rec.MiddleName,
                LastName = rec.LastName,
                Department = rec.Department,
                Mail = rec.Mail,
             //   Password = rec.Password,
                Requests = context.Requests.Where(recPC => recPC.TeacherId == rec.Id).Select(recPC => new RequestViewModel
                {
                    Id = recPC.Id,
                    CourseId = recPC.CourseId,
                    TeacherId = recPC.TeacherId
                }).ToList(),
                Studies = context.Studies.Where(recPC => recPC.TeacherId == rec.Id).Select(recPC => new StudyViewModel
                {
                    Id = recPC.Id,
                    Name = recPC.Name,
                    Orientation = recPC.Orientation,
                    TeacherId = recPC.TeacherId,
                    Course = context.Courses.Where(r => r.Id == rec.Id).Select(r => new CourseViewModel
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Content = r.Content,
                        StartCourse = SqlFunctions.DateName("dd", r.StartCourse) + " " + SqlFunctions.DateName("mm", r.StartCourse) + " " + SqlFunctions.DateName("yyyy", r.StartCourse),
                        EndCourse = r.EndCourse == null ? "" : SqlFunctions.DateName("dd", r.EndCourse) + " " + SqlFunctions.DateName("mm", r.EndCourse) + " " + SqlFunctions.DateName("yyyy", r.EndCourse),
                        Student_Count = r.Student_Count,
                        StudyId = r.StudyId
                    }).ToList(),
                }).ToList()
            }).ToList();
            return result;
        }

        public void UpdElement(TeacherBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Teacher element = context.Teachers.FirstOrDefault(rec =>
                        rec.FirstName == model.FirstName && rec.MiddleName == model.MiddleName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Teachers.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.FirstName = model.FirstName;
                    element.LastName = model.LastName;
                    element.MiddleName = model.MiddleName;
                    element.Mail = model.Mail;
                   // element.Password = model.Password;
                    element.Department = model.Department;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.Studies.Select(rec =>
                        rec.Id).Distinct();
                    var updateStudys = context.Studies.Where(rec =>
                        rec.TeacherId == model.Id && compIds.Contains(rec.Id));
                    foreach (var updateStudy in updateStudys)
                    {
                        updateStudy.Name =
                        model.Studies.FirstOrDefault(rec => rec.Id == updateStudy.Id).Name;
                        updateStudy.Orientation =
                        model.Studies.FirstOrDefault(rec => rec.Id == updateStudy.Id).Orientation;
                    }
                    context.SaveChanges();
                    context.Studies.RemoveRange(context.Studies.Where(rec =>
                        rec.TeacherId == model.Id && !compIds.Contains(rec.Id)));
                    context.SaveChanges();
                    //новые записи
                    //var groupStudys = model.Studies
                    //    .Where(rec => rec.Id == 0)
                    //    .GroupBy(rec => rec.Id)
                    //    .Select(rec => new
                    //    {
                    //        StudyId = rec.Key
                    //    });
                    //foreach (var groupStudy in groupStudys)
                    //{
                    //    Study elementPC = context.Studies
                    //        .FirstOrDefault(rec => rec.TeacherId == model.Id
                    //                        && rec.Id == groupStudy.StudyId);
                    //    if (elementPC != null)
                    //    {
                    //        elementPC.Name = groupStudy.;
                    //        context.SaveChanges();
                    //    }
                    //    else
                    //    {
                    //        context.Studies.Add(new TeacherStudy
                    //        {
                    //            TeacherId = model.Id,
                    //            StudyId = groupStudy.StudyId,
                    //            Count = groupStudy.Count
                    //        });
                    //        context.SaveChanges();
                    //    }
                    //}
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
