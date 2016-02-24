namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using Data.Models;

    public interface IBugReportService
    {
        IQueryable<BugReport> GetAll();

        void Create(string content, string email);
    }
}
