namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using UniversityStudentSystem.Data.Models;

    public interface IHomePageService
    {
        IQueryable<News> GetTopNews();

        IQueryable<ForumPost> GetTopForumPosts();
    }
}
