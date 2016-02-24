namespace UniversityStudentSystem.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;
    using UniversityStudentSystem.Web.Models.Courses;

    public class CoursesController : BaseController
    {
        private ICoursesService courseService;

        public CoursesController(ICoursesService courseService)
        {
            this.courseService = courseService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var courses = this.courseService.GetAll().To<CourseViewModel>().ToList();
            return this.Json(courses.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CourseViewModel model)
        {
            this.courseService.DeleteById(model.Id);
            RouteValueDictionary routeValues = this.GridRouteValues();
            return this.RedirectToAction("Index", routeValues);
        }
    }
}