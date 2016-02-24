namespace UniversityStudentSystem.Services
{
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

        public void DeleteById(int id)
        {
            var newsItem = this.newsRepository.GetById(id);
            this.newsRepository.Delete(newsItem);
            this.newsRepository.Save();
        }

        public IQueryable<News> GetAll()
        {
            return this.newsRepository.All();
        }
    }
}
