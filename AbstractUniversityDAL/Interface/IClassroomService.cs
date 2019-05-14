using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.Interface
{
    public interface IClassroomService
    {
        List<ClassroomViewModel> GetList();
        ClassroomViewModel GetElement(int id);
        void AddElement(ClassroomBindingModel model);
        void UpdElement(ClassroomBindingModel model);
        void DelElement(int id);
    }
}
