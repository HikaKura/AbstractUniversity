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
    public class ClassroomServiceDB : IClassroomService
    {
        private UniversityDbContext context;

        public ClassroomServiceDB(UniversityDbContext context)
        {
            this.context = context;
        }

        public List<ClassroomViewModel> GetList()
        {
            List<ClassroomViewModel> result = context.Classrooms.Select(rec => new ClassroomViewModel
            {
                Id = rec.Id,
                Number = rec.Number,
                Status = rec.Status,
                Pavilion = rec.Pavilion,
                ClassroomCourses = context.ClassroomCourses.Where(recPC => recPC.ClassroomId == rec.Id).Select(recPC => new ClassroomCourseViewModel
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
            Classroom element = context.Classrooms.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ClassroomViewModel
                {
                    Id = element.Id,
                    Number = element.Number,
                    Status = element.Status,
                    Pavilion = element.Pavilion,
                    ClassroomCourses = context.ClassroomCourses.Where(recPC => recPC.ClassroomId == element.Id).Select(recPC => new ClassroomCourseViewModel
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
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Classroom element = context.Classrooms.FirstOrDefault(rec => rec.Number == model.Number);
                    if (element != null)
                    {
                        throw new Exception("Уже есть аудитория с таким номером");
                    }
                    element = new Classroom
                    {
                        Number = model.Number,
                        Status = model.Status,
                        Pavilion = model.Pavilion
                    };
                    context.Classrooms.Add(element);
                    context.SaveChanges();
                    context.ClassroomCourses.Add(new ClassroomCourse
                    {
                        ClassroomId = element.Id,
                        Number = element.Number
                        // Name = model.ClassroomCourses.Select(r => r.Name)
                    });
                    context.SaveChanges();
                    // убираем дубли по курсам
                    /*var groupCourses = model.ClassroomCourses.GroupBy(rec => rec.CourseId).Select(rec => new
                    {
                        CourseId = rec.Key,
                        Name = rec.Where(recPC => recPC.Id == rec.Key).Select(r => r.Name)
                    });
                    // добавляем курсы
                    foreach (var groupCourse in groupCourses)
                    {
                        context.ClassroomCourses.Add(new ClassroomCourse
                        {
                            ClassroomId = element.Id,
                            CourseId = groupCourse.CourseId,
                            // Name = model.ClassroomCourses.Select(r => r.Name)
                        });
                        context.SaveChanges();
                    }*/
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

                    Classroom element = context.Classrooms.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по курсам при удалении аудитории
                        context.ClassroomCourses.RemoveRange(context.ClassroomCourses.Where(rec => rec.Id == id));
                        context.Classrooms.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Аудитория не найдена");
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

        public void UpdElement(ClassroomBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Classroom element = context.Classrooms.FirstOrDefault(rec => rec.Number == model.Number && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть аудитория с таким номером");
                    }
                    element = context.Classrooms.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Аудитория не найдена");
                    }
                    element.Number = model.Number;
                    element.Status = model.Status;
                    element.Pavilion = model.Pavilion;
                    context.SaveChanges();

                    // обновляем существуюущие курсы
                    var compIds = model.ClassroomCourses.Select(rec => rec.CourseId).Distinct();
                    var updateCourses = context.ClassroomCourses.Where(rec => rec.ClassroomId == model.Id && compIds.Contains(rec.CourseId));
                    foreach (var updateCourse in updateCourses)
                    {
                        updateCourse.Name = model.ClassroomCourses.FirstOrDefault(rec => rec.Id == updateCourse.Id).Name;
                    }
                    context.SaveChanges();
                    context.ClassroomCourses.RemoveRange(context.ClassroomCourses.Where(rec => rec.ClassroomId == model.Id && !compIds.Contains(rec.CourseId)));
                    context.SaveChanges();
                    // новые записи
                    var groupCourses = model.ClassroomCourses.Where(rec => rec.Id == 0).GroupBy(rec => rec.CourseId).Select(rec => new
                    {
                        CourseId = rec.Key,
                        //Name = rec.Name.ToString()
                    });
                    foreach (var groupCourse in groupCourses)
                    {
                        ClassroomCourse elementPC = context.ClassroomCourses.FirstOrDefault(rec => rec.ClassroomId == model.Id && rec.CourseId == groupCourse.CourseId);
                        if (elementPC != null)
                        {
                            //  elementPC.Name += groupCourse.Name;
                        }
                        else
                        {
                            context.ClassroomCourses.Add(new ClassroomCourse
                            {
                                ClassroomId = model.Id,
                                CourseId = groupCourse.CourseId,
                                // Name = groupCourse.Name
                            });
                            context.SaveChanges();
                        }
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
