namespace UniversityStudentSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Infrastructure.Mapping;
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

        [ChildActionOnly]
        // [OutputCache(Duration = WebConstants.HomePageCacheDuration)]
        public ActionResult SideMenu()
        {
            SideMenuViewModel sideMenu = new SideMenuViewModel();
            sideMenu.Courses = coursesService
                .GetAll()
                .OrderByDescending(c => c.CreatedOn)
                .Take(5)
                .To<CourseViewModel>()
                .ToList();

            sideMenu.Specialties = specialtiesService
                .GetAll()
                .OrderByDescending(c => c.CreatedOn)
                .Take(5)
                .To<SpecialtyViewModel>()
                .ToList();

            var trainerRole = usersService.GetRoles().FirstOrDefault(r => r.Name == RoleConstants.Trainer);
            if (trainerRole != null)
            {
                sideMenu.Trainers = usersService
                        .GetAll()
                        .Where(u => u.Roles.Any(r => r.RoleId == trainerRole.Id))
                        .OrderByDescending(c => c.CreatedOn)
                        .Take(5)
                        .To<UserViewModel>()
                        .ToList();
            }  

            return this.PartialView("_SideMenu", sideMenu);
        }
    }
}