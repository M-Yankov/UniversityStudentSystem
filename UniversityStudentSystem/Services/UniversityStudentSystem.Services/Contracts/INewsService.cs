namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using UniversityStudentSystem.Data.Models;

    public interface INewsService
    {
        IQueryable<News> GetAll();

        int Create(News modelToSave);
    }
}
