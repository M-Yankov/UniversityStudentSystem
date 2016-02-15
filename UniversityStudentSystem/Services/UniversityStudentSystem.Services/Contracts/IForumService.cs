namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using UniversityStudentSystem.Data.Models;

    public interface IForumService
    {
        IQueryable<ForumPost> GetAll();

        void PostComment(string content, int postId, string userId);

        int Create(string title, string content, int categoryId, string userId);
    }
}
