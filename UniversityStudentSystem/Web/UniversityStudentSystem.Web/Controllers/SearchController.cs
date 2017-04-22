namespace UniversityStudentSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Models;
    using Models.Courses;
    using Models.ForumPosts;
    using Models.NewsModels;
    using Models.Specialties;
    using Models.Users;
    using UniversityStudentSystem.Common;
    using UniversityStudentSystem.Services.Contracts;

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
            if (text == null || text.Trim().Length < WebConstants.MinTextLength)
            {
                this.TempData["Error"] = $"Type more than { WebConstants.MinTextLength } symbols for searching";
                return this.View(model);
            }

            model.Criteria = text;
            text = text.Trim().ToLower();
            model.Courses = this.searchService.GetCourses(text).To<CourseViewModel>().ToList();
            model.Trainers = this.searchService.GetTrainers(text).To<UserViewModel>().ToList();
            model.News = this.searchService.GetNews(text).To<NewsViewModel>().ToList();
            model.ForumPosts = this.searchService.GetForumPosts(text).To<ForumPostViewModel>().ToList();
            model.Specialties = this.searchService.GetSpecialties(text).To<SpecialtyViewModel>().ToList();

            return this.View(model);
        }
    }
}