namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using UniversityStudentSystem.Data.Models;

    public interface ICoursesService
    {
        IQueryable<Course> GetAll();

        void AddTask(CourseTask task, int id);

        void JoinIn(int courseId, string userId);

        void Edit(Course model);

        void AddResourse(string name, string path, int id);

        string IsAllowed(string userId, int courseId);

        void SaveSolution(string path, string userId, int courseId);

        string SolutionResult(string userId, int courseId);

        void AddMark(int value, string username, int courseId, string reason);
    }
}
