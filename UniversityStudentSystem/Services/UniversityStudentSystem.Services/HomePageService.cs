namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Repositories;
    using Data.Models;
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
