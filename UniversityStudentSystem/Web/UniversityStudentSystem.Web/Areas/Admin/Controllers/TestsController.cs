namespace UniversityStudentSystem.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;

    public class TestsController : BaseController
    {
        private ITestService testService;

        public TestsController(ITestService testService)
        {
            this.testService = testService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var courses = this.testService.GetAll().To<TestAdminModel>().ToList();
            return this.Json(courses.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TestAdminModel test)
        {
            this.testService.DeleteById(test.Id);
            RouteValueDictionary routeValues = this.GridRouteValues();
            return this.RedirectToAction("Index", routeValues);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, TestAdminModel test)
        {
            if (ModelState.IsValid)
            {
                this.testService.Update(test.Id, test.StartDate, test.EndDate, test.Name, test.IsEnabled);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return this.RedirectToAction("Index", routeValues);
            }

            /*var courses = this.testService.GetAll().To<TestAdminModel>().ToList();
            return Json(courses.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);*/
            return this.RedirectToAction("Index");
        }
    }
}