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

    public class NewsController : BaseController
    {
        private INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }
        
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var courses = this.newsService.GetAll().To<KendoNewsModel>().ToList();
            return this.Json(courses.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, KendoNewsModel model)
        {
            this.newsService.DeleteById(model.Id);
            RouteValueDictionary routeValues = this.GridRouteValues();
            return this.RedirectToAction("Index", routeValues);
        }
    }
}