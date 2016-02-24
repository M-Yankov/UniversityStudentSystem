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
    using Web.Models.NewsModels;
    using Models;

    public class NewsController : BaseController
    {
        private INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        // GET: Admin/News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var courses = this.newsService.GetAll().To<KendoNewsModel>().ToList();
            return Json(courses.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, KendoNewsModel model)
        {
            this.newsService.DeleteById(model.Id);
            RouteValueDictionary routeValues = this.GridRouteValues();
            return RedirectToAction("Index", routeValues);
        }
    }
}