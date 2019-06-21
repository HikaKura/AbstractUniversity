using AbstractUniversity;
using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.Interface;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityImplementList.Implementation
{
    public class MainServiceDB : IMainService
    {
        private UniversityDbContext context;

        public MainServiceDB(UniversityDbContext context)
        {
            this.context = context;
        }

        public List<CourseViewModel> GetList()
        {
            List<CourseViewModel> result = context.Courses.Select(rec => new CourseViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Status = rec.Status.ToString(),
                StartCourse = SqlFunctions.DateName("dd", rec.StartCourse) + " " + SqlFunctions.DateName("mm", rec.StartCourse) + " " + SqlFunctions.DateName("yyyy", rec.StartCourse),
                EndCourse = rec.EndCourse == null ? "" : SqlFunctions.DateName("dd", rec.EndCourse) + " " + SqlFunctions.DateName("mm", rec.EndCourse) + " " + SqlFunctions.DateName("yyyy", rec.EndCourse),
                Content = rec.Content,
                Student_Count = rec.Student_Count,
                StudyName = rec.Study.Name,
                TeacherFIO = rec.Study.Teacher.LastName,
                StudyId = rec.StudyId,
                ClassroomCourses = context.ClassroomCourses.Where(rep => rep.CourseId == rec.Id).Select(rep => new ClassroomCourseViewModel
                {
                    Id = rep.Id,
                    CourseId = rep.CourseId,
                    ClassroomId = rep.ClassroomId,
                    // Number = rep.Number
                }).ToList(),
                ClassroomId = rec.ClassroomId
            }).ToList();
            return result;
        }

        public void NotBeginCourse(CourseBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Course element = context.Courses.FirstOrDefault(rec => rec.Name == model.Name);
                    if (element != null)
                    {
                        throw new Exception("Уже есть курс с таким названием");
                    }
                    element = new Course
                    {
                        Name = model.Name,
                        StartCourse = DateTime.Now,
                        Content = model.Content,
                        Student_Count = model.Student_Count,
                        StudyId = model.StudyId,
                        ClassroomId = model.ClassroomId,
                        Status = CourseStatus.NotBegin

                    };
                    context.Courses.Add(element);
                    context.SaveChanges();
                    try
                    {
                        var groupClassrooms = model.ClassroomCourses.GroupBy(rec => rec.ClassroomId).Select(rec => new
                        {
                            ClassroomId = rec.Key,
                            //Number = rec.Key
                        });

                        // добавляем компоненты
                        foreach (var groupClassroom in groupClassrooms)
                        {
                            context.ClassroomCourses.Add(new ClassroomCourse
                            {
                                CourseId = element.Id,
                                ClassroomId = groupClassroom.ClassroomId,
                                // Name = element.Name,
                                //Number = groupClassroom.Number
                            });
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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

        public void CourseGoing(CourseBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Course element = context.Courses.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Курс не найден");
                    }
                    if (element.Status != CourseStatus.NotBegin)
                    {
                        throw new Exception("Курс еще \"не начался\"");
                    }
                    element.Status = CourseStatus.Going;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void CourseFinished(CourseBindingModel model)
        {
            Course element = context.Courses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != CourseStatus.Going)
            {
                throw new Exception("Заказ еще не \"идет\"");
            }
            element.EndCourse = DateTime.Now;
            element.Status = CourseStatus.Finished;
            context.SaveChanges();
        }

        public bool Check(CourseBindingModel model)
        {
            List<CourseViewModel> result = context.Courses.Select(rec => new CourseViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                StartCourse = SqlFunctions.DateName("dd", rec.StartCourse) + " " + SqlFunctions.DateName("mm", rec.StartCourse) + " " + SqlFunctions.DateName("yyyy", rec.StartCourse),
                EndCourse = rec.EndCourse == null ? "" : SqlFunctions.DateName("dd", rec.EndCourse) + " " + SqlFunctions.DateName("mm", rec.EndCourse) + " " + SqlFunctions.DateName("yyyy", rec.EndCourse),
                Content = rec.Content,
                Student_Count = rec.Student_Count,
                StudyId = rec.StudyId
            }).ToList();
            /* int hourEnd = Convert.ToInt32(model.EndCourse.Substring(0, 2));
             DateTime date = DateTime.Now;
             int hourNow = Convert.ToInt32(date.ToString("dd"));
             if (hourNow - hourEnd != 0)
             {
                 return true;
             }*/
            return false;
        }
    }
}
