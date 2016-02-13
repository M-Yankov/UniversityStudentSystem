namespace UniversityStudentSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common;
    using Infrastructure.Mapping;
    using Models.ForumPosts;
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
        // [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult LatestNews()
        {
            IList<NewsViewModel> news = homeService
                .GetTopNews()
                .OrderByDescending(n => n.CreatedOn)
                .Take(WebConstants.TopNewsCount)
                .To<NewsViewModel>().ToList();
            return this.PartialView("_LatestNews", news);
        }

        [ChildActionOnly]
        // [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult LatestForumPosts()
        {
            IList<ForumPostViewModel> forumPosts = homeService
                .GetTopForumPosts()
                .OrderByDescending(f => f.CreatedOn)
                .Take(WebConstants.TopForumPostsCount)
                .To<ForumPostViewModel>().ToList();
            return this.PartialView("_LatestForumPosts", forumPosts);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}