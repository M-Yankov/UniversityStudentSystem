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
        public ICollection<UserViewModel> Trainers { get; set; }

        public ICollection<CourseViewModel> Courses { get; set; }

        public ICollection<SpecialtyViewModel> Specialties { get; set; }

        public ICollection<ForumPostViewModel> ForumPosts { get; set; }

        public ICollection<NewsViewModel> News { get; set; }
    }
}