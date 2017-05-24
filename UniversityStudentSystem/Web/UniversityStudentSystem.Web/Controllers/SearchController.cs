namespace UniversityStudentSystem.Web.Controllers
{
    using System.Collections.Generic;
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

        public ActionResult Index(string types = null, string text = null)
        {
            var model = new SearchResultModel();
            if (text == null || text.Trim().Length < WebConstants.MinTextLength)
            {
                this.TempData["Error"] = $"Type more than { WebConstants.MinTextLength } symbols for searching";
                return this.View(model);
            }

            if (!string.IsNullOrEmpty(types))
            {
                text = text.Trim().ToLower();
                model.Criteria = text;
                model.Courses =  types.Contains(SearchResultModel.CourseSearchKey) ? this.searchService.GetCourses(text).To<CourseViewModel>().ToList() : new List<CourseViewModel>();
                model.Trainers = types.Contains(SearchResultModel.TrainersSearchKey) ? this.searchService.GetTrainers(text).To<UserViewModel>().ToList() : new List<UserViewModel>();
                model.News = types.Contains(SearchResultModel.NewsSearchKey) ? this.searchService.GetNews(text).To<NewsViewModel>().ToList() : new List<NewsViewModel>();
                model.ForumPosts = types.Contains(SearchResultModel.ForumPostSearchKey) ? this.searchService.GetForumPosts(text).To<ForumPostViewModel>().ToList() : new List<ForumPostViewModel>();
                model.Specialties = types.Contains(SearchResultModel.SpecialtySearchKey) ? this.searchService.GetSpecialties(text).To<SpecialtyViewModel>().ToList() : new List<SpecialtyViewModel>();
            }

            return this.View(model);
        }
    }
}