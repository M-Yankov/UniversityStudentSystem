namespace UniversityStudentSystem.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using UniversityStudentSystem.Web.Models.Courses;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;
    using Web.Models.Specialties;

    public class SpecialtiesController : BaseController
    {
        private ISpecialtiesService specialtiesService;

        public ActionResult Index()
        {
            return this.View();
        }

        public SpecialtiesController(ISpecialtiesService specialtiesService)
        {
            this.specialtiesService = specialtiesService;
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var courses = this.specialtiesService.GetAll().To<SpecialtyViewModel>().ToList();
            return Json(courses.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, SpecialtyViewModel model)
        {
            this.specialtiesService.DeleteById(model.Id);
            RouteValueDictionary routeValues = this.GridRouteValues();
            return RedirectToAction("Index", routeValues);
        }
    }
}