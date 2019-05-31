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
                StartCourse = SqlFunctions.DateName("dd", rec.StartCourse) + " " + SqlFunctions.DateName("mm", rec.StartCourse) + " " +  SqlFunctions.DateName("yyyy", rec.StartCourse),
                EndCourse = rec.EndCourse == null ? "" : SqlFunctions.DateName("dd", rec.EndCourse) + " " + SqlFunctions.DateName("mm", rec.EndCourse) + " " + SqlFunctions.DateName("yyyy", rec.EndCourse),
                Content = rec.Content,
                Student_Count = rec.Student_Count,
                StudyId = rec.StudyId
            }).ToList();
            return result;
        }

        public void NotBeginCourse(CourseBindingModel model)
        {
            context.Courses.Add(new Course
            {
                Name = model.Name,
                StartCourse = DateTime.Now,
                Content = model.Content,
                Student_Count = model.Student_Count,
                StudyId = model.StudyId,
                Status = CourseStatus.NotBegin
            });
            context.SaveChanges();
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
    }
}
