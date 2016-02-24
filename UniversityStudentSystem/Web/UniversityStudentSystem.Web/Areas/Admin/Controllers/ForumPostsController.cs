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
    using Web.Models.ForumPosts;
    using Models;

    public class ForumPostsController : BaseController
    {
        private IForumService forumService;

        public ForumPostsController(IForumService forumService)
        {
            this.forumService = forumService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var courses = this.forumService.GetAll().To<KendoForumModel>().ToList();
            return Json(courses.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, KendoForumModel model)
        {
            this.forumService.DeleteById(model.Id);
            RouteValueDictionary routeValues = this.GridRouteValues();
            return RedirectToAction("Index", routeValues);
        }
    }
}
