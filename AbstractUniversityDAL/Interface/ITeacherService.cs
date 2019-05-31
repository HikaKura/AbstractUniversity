using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.Interface
{
    public interface ITeacherService
    {
        List<TeacherViewModel> GetList();
        TeacherViewModel GetElement(int id);
        void AddElement(TeacherBindingModel model);
        void UpdElement(TeacherBindingModel model);
        void DelElement(int id);
    }
}
