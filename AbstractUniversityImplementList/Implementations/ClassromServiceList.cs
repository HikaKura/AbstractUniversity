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
            }).ToList();
            return result;
        }

        public void AddElement(ClassroomBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void DelElement(int id)
        {
            throw new NotImplementedException();
        }

        public ClassroomViewModel GetElement(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdElement(ClassroomBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
