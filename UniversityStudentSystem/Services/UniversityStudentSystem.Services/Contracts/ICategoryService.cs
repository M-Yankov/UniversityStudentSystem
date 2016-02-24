namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;

    using Data.Models;

    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
    }
}
