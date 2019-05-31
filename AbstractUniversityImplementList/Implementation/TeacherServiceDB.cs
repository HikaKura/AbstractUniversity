using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.Interface;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractUniversityImplementList.Implementation
{
    class TeacherServiceDB : ITeacherService
    {
        private UniversityDbContext context;

        public TeacherServiceDB(UniversityDbContext context)
        {
            this.context = context;
        }

        public void AddElement(TeacherBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void DelElement(int id)
        {
            throw new NotImplementedException();
        }

        public TeacherViewModel GetElement(int id)
        {
            throw new NotImplementedException();
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
                Password = rec.Password,
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
                        StartCourse = r.StartCourse,
                        EndCourse = r.EndCourse,
                        Student_Count = r.Student_Count,
                        StudyId = r.StudyId
                    }).ToList(),
                }).ToList()
            }).ToList();
            return result;
        }

        public void UpdElement(TeacherBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
