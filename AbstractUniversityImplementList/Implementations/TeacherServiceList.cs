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
    public class TeacherServiceList : ITeacherService
    {
        private DataListSingleton source;
        public TeacherServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<TeacherViewModel> GetList()
        {
            List<TeacherViewModel> result = source.Teachers.Select(rec => new TeacherViewModel
            {
                Id = rec.Id,
                FirstName = rec.FirstName,
                MiddleName = rec.MiddleName,
                LastName = rec.LastName,
                Mail = rec.Mail,
                Password = rec.Password,
                Department = rec.Department
            }).ToList();
            return result;
        }
        public TeacherViewModel GetElement(int id)
        {
            Teacher element = source.Teachers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new TeacherViewModel
                {
                    Id = element.Id,
                    FirstName = element.FirstName,
                    MiddleName = element.MiddleName,
                    LastName = element.LastName,
                    Mail = element.Mail,
                    Password = element.Password,
                    Department = element.Department
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(TeacherBindingModel model)
        {
            Teacher element = source.Teachers.FirstOrDefault(rec => rec.FirstName == model.FirstName);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Teachers.Count > 0 ? source.Teachers.Max(rec => rec.Id) : 0;
            source.Teachers.Add(new Teacher
            {
                Id = maxId + 1,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName =  model.LastName,
                Mail = model.Mail,
                Password = model.Password,
                Department = model.Department
            });
        }
        public void UpdElement(TeacherBindingModel model)
        {
            Teacher element = source.Teachers.FirstOrDefault(rec => rec.FirstName == model.FirstName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Teachers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.FirstName = model.FirstName;
            element.LastName = model.LastName;
            element.MiddleName = model.MiddleName;
            element.Mail = model.Mail;
            element.Password = model.Password;
            element.Department = model.Department;
        }
        public void DelElement(int id)
        {
            Teacher element = source.Teachers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Teachers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
