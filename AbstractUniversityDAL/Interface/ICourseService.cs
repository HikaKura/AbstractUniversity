using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.Interface
{
    public interface ICourseService
    {
        List<CourseViewModel> GetList();
        CourseViewModel GetElement(int id);
        void AddElement(CourseBindingModel model);
        void UpdElement(CourseBindingModel model);
        void CheckElement(CourseBindingModel model);
        void DelElement(int id);
    }
}
