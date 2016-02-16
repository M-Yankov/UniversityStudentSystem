namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class BugReportService : IBugReportService
    {
        private IRepository<BugReport> bugsRepository;

        public BugReportService(IRepository<BugReport> bugsRepository)
        {
            this.bugsRepository = bugsRepository;
        }

        public void Create(string content, string email)
        {
            var bug = new BugReport()
            {
                Content = content,
                CreatedOn = DateTime.Now,
                Email = email,
            };

            this.bugsRepository.Add(bug);

            this.bugsRepository.Save();    
        }

        public IQueryable<BugReport> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
