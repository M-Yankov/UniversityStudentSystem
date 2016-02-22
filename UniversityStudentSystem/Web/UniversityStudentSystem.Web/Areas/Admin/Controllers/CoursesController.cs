namespace UniversityStudentSystem.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models.Courses;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;

    public class CoursesController : BaseController
    {
        private ICoursesService courseService;

        public CoursesController(ICoursesService courseService)
        {
            this.courseService = courseService;
        }

        // GET: Admin/Courses
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var courses = courseService.GetAll().To<CourseViewModel>().ToList();
            return Json(courses.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CourseViewModel> courses)
        {
            
            return Json(courses.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}