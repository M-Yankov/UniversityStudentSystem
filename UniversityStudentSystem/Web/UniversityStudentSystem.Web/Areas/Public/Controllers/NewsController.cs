namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Common;
    using Common.Extensions;
    using Data.Models;
    using HelperProviders;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models.NewsModels;
    using Services.Contracts;
    using Web.Controllers;

    public class NewsController : BaseController
    {
        private INewsService newsService;

        public NewsController(INewsService news)
        {
            this.newsService = news;
        }

        // GET: Public/News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetNews([DataSourceRequest] DataSourceRequest request)
        {
            var news = this.newsService
                .GetAll()
                .OrderByDescending(n => n.CreatedOn)
                .To<NewsViewModel>()
                .ToList();

            return this.Json(news.ToDataSourceResult(request));
        }
        
        public ActionResult Details(int id)
        {
            var newsFromDb = this.newsService.GetAll().Where(n => n.Id == id).FirstOrDefault();
            if (newsFromDb == null)
            {
                return this.RedirectToAction("NotFound");
            }

            var newsDetails = this.Mapper.Map<NewsViewModel>(newsFromDb);

            return View(newsDetails);
        }

        [Authorize(Roles = RoleConstants.Admin + ", " + RoleConstants.Trainer)]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConstants.Admin + ", " + RoleConstants.Trainer)]
        public ActionResult Create(NewsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.File != null)
            {
                model.PhotoPath = new NewsImageUploader().Save(this.Server, model.File);
            }

            var dbNews = this.Mapper.Map<News>(model);
            dbNews.CreatedOn = DateTime.Now;
            int id = this.newsService.Create(dbNews);

            return this.RedirectToAction("Details", "News", new { id = id, name = model.Title.ToUrl() });
        }
    }
}