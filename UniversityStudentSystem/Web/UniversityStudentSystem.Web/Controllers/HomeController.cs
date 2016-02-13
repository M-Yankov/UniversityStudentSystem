namespace UniversityStudentSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common;
    using Infrastructure.Mapping;
    using Models.Home;
    using Models.NewsModels;
    using UniversityStudentSystem.Services.Contracts;

    public class HomeController : Controller
    {
        private IHomePageService homeService;

        public HomeController(IHomePageService service)
        {
            this.homeService = service;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = WebConstants.CacheTimeDuration)]
        public ActionResult LatestNews()
        {
            IList<NewsViewModel> news = homeService.GetTopNews()
                .Take(WebConstants.TopNewsCount)
                .To<NewsViewModel>().ToList();
            return this.PartialView("_LatestNews", news);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}