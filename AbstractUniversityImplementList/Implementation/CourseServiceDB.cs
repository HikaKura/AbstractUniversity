using AbstractUniversity;
using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.Interface;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityImplementList.Implementation
{
    public class CourseServiceDB : ICourseService
    {
        private UniversityDbContext context;

        public CourseServiceDB(UniversityDbContext context)
        {
            this.context = context;
        }

        public List<CourseViewModel> GetList()
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
            return result;
        }

        public CourseViewModel GetElement(int id)
        {
            Course element;
            CourseViewModel course;
            using (var transaction = context.Database.BeginTransaction())
            {
                element = context.Courses.Where(rec => rec.Id == id).FirstOrDefault();
                course = new CourseViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    StartCourse = SqlFunctions.DateName("dd", element.StartCourse) + " " + SqlFunctions.DateName("mm", element.StartCourse) + " " + SqlFunctions.DateName("yyyy", element.StartCourse),
                    EndCourse = element.EndCourse == null ? "" : SqlFunctions.DateName("dd", element.EndCourse) + " " + SqlFunctions.DateName("mm", element.EndCourse) + " " + SqlFunctions.DateName("yyyy", element.EndCourse),
                    Content = element.Content,
                    Student_Count = element.Student_Count,
                    StudyId = element.StudyId
                };
            }
            return course;
        }

        public void AddElement(CourseBindingModel model)
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
                    context.Courses.Add(new Course
                    {
                        Name = model.Name,
                        StartCourse = DateTime.Parse(model.StartCourse),
                        EndCourse = DateTime.Parse(model.EndCourse),
                        Content = model.Content,
                        Student_Count = model.Student_Count,
                        StudyId = model.StudyId
                    });
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

        public void UpdElement(CourseBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Course element = context.Courses.FirstOrDefault(rec => rec.Name == model.Name && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть курс с таким названием");
                    }
                    element = context.Courses.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.Name = model.Name;
                    element.StartCourse = DateTime.Parse(model.StartCourse);
                    element.EndCourse = DateTime.Parse(model.EndCourse);
                    element.Content = model.Content;
                    element.Student_Count = model.Student_Count;
                    element.StudyId = model.StudyId;
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

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Course element = context.Courses.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по курсам при удалении аудитории
                        context.Courses.RemoveRange(context.Courses.Where(rec => rec.Id == id));
                        context.Courses.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Курс не найден");
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
