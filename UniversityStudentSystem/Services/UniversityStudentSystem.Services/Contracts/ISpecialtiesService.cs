namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using UniversityStudentSystem.Data.Models;

    public interface ISpecialtiesService
    {
        IQueryable<Specialty> GetAll();

        IQueryable<Course> GetAllCoursesForUser(string id);
    }
}
