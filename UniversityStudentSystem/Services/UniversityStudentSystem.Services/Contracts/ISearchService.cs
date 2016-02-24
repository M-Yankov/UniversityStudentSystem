namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using UniversityStudentSystem.Data.Models;

    public interface ISearchService 
    {
        IQueryable<News> GetNews(string text);

        IQueryable<User> GetTrainers(string text);

        IQueryable<ForumPost> GetForumPosts(string text);

        IQueryable<Course> GetCourses(string text);

        IQueryable<Specialty> GetSpecialties(string text);
    }
}
