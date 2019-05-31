using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.Interface
{
    public interface IStudyService
    {
        List<StudyViewModel> GetList();
        StudyViewModel GetElement(int id);
        void AddElement(StudyBindingModel model);
        void UpdElement(StudyBindingModel model);
        void DelElement(int id);
    }
}
