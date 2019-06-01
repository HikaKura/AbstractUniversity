using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversetyWebView.Controllers
{
    public class CourseController : Controller
    {

        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        // GET: Course
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            _service.AddElement(new CourseBindingModel
            {
                Name = Request["Name"],
                Content = Request["Content"],
                Student_Count = int.Parse(Request["Student_Count"]),
                StudyId = Request["StudyId"] != null && !Request["StudyId"].Equals("") ?  int.Parse(Request["StudyId"]) : default(int?),
                StartCourse = Request["StartCourse"],
                EndCourse = Request["EndCourse"],
            });
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = _service.GetElement(id);
            var bindingModel = new CourseBindingModel
            {
                Id = id,
                Name = viewModel.Name,
                Content = viewModel.Content,
                Student_Count = viewModel.Student_Count,
                StudyId = viewModel.StudyId
                
            };
            return View(bindingModel);
        }

        [HttpPost]
        public ActionResult EditPost()
        {
            _service.UpdElement(new CourseBindingModel
            {
                Id = int.Parse(Request["Id"]),
                Name = Request["Name"],
                Content = Request["Content"],
                Student_Count = int.Parse(Request["Student_Count"]),
                StudyId = int.Parse(Request["StudyId"])
            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}
