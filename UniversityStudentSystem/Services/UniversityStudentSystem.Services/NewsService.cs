
namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data.Models;
    using UniversityStudentSystem.Data.Repositories;

    public class NewsService : INewsService
    {
        private IRepository<News> newsRepository;

        public NewsService(IRepository<News> newsRepo)
        {
            this.newsRepository = newsRepo;
        }

        public int Create(News modelToSave)
        {
            this.newsRepository.Add(modelToSave);
            this.newsRepository.Save();

            return modelToSave.Id;
        }

        public IQueryable<News> GetAll()
        {
            return this.newsRepository.All();
        }
    }
}
