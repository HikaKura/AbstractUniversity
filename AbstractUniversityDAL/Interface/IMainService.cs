using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityDAL.Interface
{
    public interface IMainService
    {
        List<CourseViewModel> GetList();
        void NotBeginCourse(CourseBindingModel model);
        void CourseGoing(CourseBindingModel model);
        void CourseFinished(CourseBindingModel model);
    }
}
