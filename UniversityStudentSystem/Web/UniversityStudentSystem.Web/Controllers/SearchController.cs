
namespace UniversityStudentSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Models;
    using Models.Courses;
    using UniversityStudentSystem.Common;
    using UniversityStudentSystem.Services.Contracts;
    using System.Linq;
    using Models.Users;
    using Models.NewsModels;
    using Models.ForumPosts;
    using Models.Specialties;
    public class SearchController : BaseController
    {
        private ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public ActionResult Index(string text = null)
        {
            var model = new SearchResultModel();
            if (text == null || text.Length < WebConstants.MinTextLength)
            {
                this.TempData["Error"] = $"Type more than { WebConstants.MinTextLength } symbols for searching";
                return View(model);
            }

            model.Courses = searchService.GetCourses(text).To<CourseViewModel>().ToList();
            model.Trainers = searchService.GetTrainers(text).To<UserViewModel>().ToList();
            model.News = searchService.GetNews(text).To<NewsViewModel>().ToList();
            model.ForumPosts = searchService.GetForumPosts(text).To<ForumPostViewModel>().ToList();
            model.Specialties = searchService.GetSpecialties(text).To<SpecialtyViewModel>().ToList();

            return View(model);
        }
    }
}