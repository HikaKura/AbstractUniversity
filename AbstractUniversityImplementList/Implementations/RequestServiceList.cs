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
    class RequestServiceList
    {
        private DataListSingleton source;
        public RequestServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<RequestViewModel> GetList()
        {
            List<RequestViewModel> result = source.Requests.Select(rec => new RequestViewModel
            {
                Id = rec.Id,
                TeacherId = rec.TeacherId,
                CourseId = rec.CourseId
            }).ToList();
            return result;
        }
        public RequestViewModel GetElement(int id)
        {
            Request element = source.Requests.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new RequestViewModel
                {
                    Id = element.Id,
                    TeacherId = element.TeacherId,
                    CourseId = element.CourseId,
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(RequestBindingModel model)
        {
            Request element = source.Requests.FirstOrDefault(rec => rec.TeacherId == model.TeacherId);
            if (element != null)
            {
                throw new Exception("Такая заявка уже существует");
            }
            int maxId = source.Requests.Count > 0 ? source.Requests.Max(rec => rec.Id) : 0;
            source.Requests.Add(new Request
            {
                Id = maxId + 1,
                TeacherId = model.TeacherId,
                CourseId = model.CourseId
            });
        }
        public void UpdElement(RequestBindingModel model)
        {
            Request element = source.Requests.FirstOrDefault(rec => rec.TeacherId == model.TeacherId && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Такая заявка уже существует");
            }
            element = source.Requests.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.TeacherId = model.TeacherId;
            element.CourseId = model.CourseId;
        }
        public void DelElement(int id)
        {
            Request element = source.Requests.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Requests.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
