namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using System.Collections.Generic;
    using Data.Models;

    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
    }
}
