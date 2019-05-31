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
            List<ClassroomViewModel> result = new List<ClassroomViewModel>();
            for (int i = 0; i < source.Classrooms.Count; ++i)
            {
                result.Add(new ClassroomViewModel
                {
                    Id = source.Classrooms[i].Id,
                    ClientFIO = source.Clients[i].ClientFIO
                });
            }
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
