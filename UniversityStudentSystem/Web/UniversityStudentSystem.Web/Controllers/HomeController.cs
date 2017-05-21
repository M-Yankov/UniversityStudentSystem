namespace UniversityStudentSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.Courses;
    using Models.ForumPosts;
    using Models.NewsModels;
    using Models.SideMenu;
    using Models.Specialties;
    using Models.Users;
    using UniversityStudentSystem.Services.Contracts;

    public class HomeController : Controller
    {
        private IHomePageService homeService;
        private ICoursesService coursesService;
        private ISpecialtiesService specialtiesService;
        private IUserService usersService;

        public HomeController(
            IHomePageService service, 
            ICoursesService courses, 
            ISpecialtiesService specialties, 
            IUserService users)
        {
            this.homeService = service;
            this.coursesService = courses;
            this.specialtiesService = specialties;
            this.usersService = users;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult LatestNews()
        {
            IList<NewsViewModel> news = this.homeService
                .GetTopNews()
                .OrderByDescending(n => n.CreatedOn)
                .Take(WebConstants.TopNewsCount)
                .To<NewsViewModel>().ToList();
            return this.PartialView("_LatestNews", news);
        }

        [ChildActionOnly]
        [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult LatestForumPosts()
        {
            IList<ForumPostViewModel> forumPosts = this.homeService
                .GetTopForumPosts()
                .OrderByDescending(f => f.Comments.Any()
                                    ? f.Comments.OrderByDescending(c => c.CreatedOn).FirstOrDefault().CreatedOn
                                    : f.CreatedOn)
                .Take(WebConstants.TopForumPostsCount)
                .To<ForumPostViewModel>()
                .ToList();
            return this.PartialView("_LatestForumPosts", forumPosts);
        }

        [ChildActionOnly]
        [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult SideMenu()
        {
            SideMenuViewModel sideMenu = new SideMenuViewModel();
            sideMenu.Courses = this.coursesService
                .GetAll()
                .OrderByDescending(c => c.CreatedOn)
                .Take(WebConstants.DefaultCountOfItemsInNavigation)
                .To<CourseViewModel>()
                .ToList();

            sideMenu.Specialties = this.specialtiesService
                .GetAll()
                .OrderByDescending(c => c.CreatedOn)
                .Take(WebConstants.DefaultCountOfItemsInNavigation)
                .To<SpecialtyViewModel>()
                .ToList();

            IdentityRole trainerRole = this.usersService.GetRoles().FirstOrDefault(r => r.Name == RoleConstants.Trainer);
            if (trainerRole != null)
            {
                sideMenu.Trainers = this.usersService
                        .GetAll()
                        .Where(u => u.Roles.Any(r => r.RoleId == trainerRole.Id))
                        .OrderByDescending(c => c.CreatedOn)
                        .Take(WebConstants.DefaultCountOfItemsInNavigation)
                        .To<UserViewModel>()
                        .ToList();
            }  

            return this.PartialView("_SideMenu", sideMenu);
        }

        [ChildActionOnly]
        [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult CourseStatistic()
        {
            StatisticViewModel model = new StatisticViewModel()
            {
                Count = this.coursesService.GetAll().Count(),
                Text = "Courses",
                ColorClass = "panel-yellow",
                IconClass = "fa-bullhorn",
                Link = "Courses"
            };

            return this.PartialView("_Statistic", model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult ForumStatistic()
        {
            StatisticViewModel model = new StatisticViewModel()
            {
                Count = this.homeService.GetTopForumPosts().Count(),
                Text = "Forum posts",
                ColorClass = "panel-green",
                IconClass = "fa-comments-o",
                Link = "Forum"
            };

            return this.PartialView("_Statistic", model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult TrainerStatistic()
        {
            IdentityRole trainerRole = this.usersService.GetRoles().FirstOrDefault(r => r.Name == RoleConstants.Trainer);
            StatisticViewModel model = new StatisticViewModel()
            {
                Count = this.usersService
                        .GetAll()
                        .Where(u => u.Roles.Any(r => r.RoleId == trainerRole.Id)).Count(),
                Text = "Trainers",
                ColorClass = "panel-primary",
                IconClass = "fa-child",
                Link = "Trainers",
            };

            return this.PartialView("_Statistic", model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult SpecialtiesStatistic()
        {
            StatisticViewModel model = new StatisticViewModel()
            {
                Count = this.specialtiesService
                        .GetAll()
                        .Count(),
                Text = "Specialties",
                ColorClass = "panel-red",
                IconClass = "fa-rocket",
                Link = "Specialties"
            };

            return this.PartialView("_Statistic", model);
        }
    }
}