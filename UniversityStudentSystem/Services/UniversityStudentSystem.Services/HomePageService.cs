namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;
    using UniversityStudentSystem.Services.Contracts;

    public class HomePageService : IHomePageService
    {
        private IRepository<News> newsRepository;

        public HomePageService(IRepository<News> repository)
        {
            this.newsRepository = repository;
        }

        public IQueryable<News> GetTopNews()
        {
            return this.newsRepository.All();
        }
    }
}
