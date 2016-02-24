namespace UniversityStudentSystem.Services
{
    using System.Linq;

    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class HomePageService : IHomePageService
    {
        private IRepository<News> newsRepository;
        private IRepository<ForumPost> forumPostsRepository;

        public HomePageService(IRepository<News> repository, IRepository<ForumPost> forumRepository)
        {
            this.newsRepository = repository;
            this.forumPostsRepository = forumRepository;
        }

        public IQueryable<ForumPost> GetTopForumPosts()
        {
            return this.forumPostsRepository.All();
        }

        public IQueryable<News> GetTopNews()
        {
            return this.newsRepository.All();
        }
    }
}
