using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.Interface
{
    public interface IReportService
    {
        void createMaterialRequest(ReportBindingModel model);
        List<CourseViewModel> GetCourse(ReportBindingModel model);
        void getCourseList(ReportBindingModel model);
    }
}
