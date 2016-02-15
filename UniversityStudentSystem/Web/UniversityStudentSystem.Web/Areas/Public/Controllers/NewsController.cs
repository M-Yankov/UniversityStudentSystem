namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
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
            var news = this.newsService.GetAll().To<NewsViewModel>().ToList();

            return this.Json(news.ToDataSourceResult(request));
        }

        // GET: Public/Details
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
    }
}