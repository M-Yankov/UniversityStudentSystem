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
    }
}
