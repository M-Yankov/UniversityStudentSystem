namespace UniversityStudentSystem.Web.Models
{
    using System.Collections.Generic;

    using Courses;
    using ForumPosts;
    using NewsModels;
    using Specialties;
    using UniversityStudentSystem.Web.Models.Users;

    public class SearchResultModel
    {
        public const string CourseSearchKey = "Courses";
        public const string SpecialtySearchKey = "Specialties";
        public const string NewsSearchKey = "News";
        public const string ForumPostSearchKey = "ForumPosts";
        public const string TrainersSearchKey = "Trainers";

        public SearchResultModel()
        {
            this.Trainers = new List<UserViewModel>();
            this.Courses = new List<CourseViewModel>();
            this.Specialties = new List<SpecialtyViewModel>();
            this.ForumPosts = new List<ForumPostViewModel>();
            this.News = new List<NewsViewModel>();
        }

        public string Criteria { get; set; }

        public ICollection<UserViewModel> Trainers { get; set; }

        public ICollection<CourseViewModel> Courses { get; set; }

        public ICollection<SpecialtyViewModel> Specialties { get; set; }

        public ICollection<ForumPostViewModel> ForumPosts { get; set; }

        public ICollection<NewsViewModel> News { get; set; }
    }
}