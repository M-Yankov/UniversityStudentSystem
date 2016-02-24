namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Common;
    using Data.Models;
    using Data.Repositories;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using UniversityStudentSystem.Services.Contracts;

    public class SearchService : ISearchService
    {
        private IRepository<News> newsRepository;
        private IRepository<Course> coursesRespository;
        private IRepository<User, string> trainersRepository;
        private IRepository<ForumPost> forumPostsRepository;
        private IRepository<Specialty> specialtiesRepository;

        public SearchService(
            IRepository<News> newsRepo,
            IRepository<Course> coursesRepo,
            IRepository<User, string> trainersRepo,
            IRepository<ForumPost> forumRepo,
            IRepository<Specialty> specialtyRepo)
        {
            this.newsRepository = newsRepo;
            this.coursesRespository = coursesRepo;
            this.trainersRepository = trainersRepo;
            this.forumPostsRepository = forumRepo;
            this.specialtiesRepository = specialtyRepo;
        }

        public IQueryable<Course> GetCourses(string text)
        {
            return this.coursesRespository.All().Where(c => c.Name.Contains(text) || c.Description.Contains(text));
        }

        public IQueryable<ForumPost> GetForumPosts(string text)
        {
            return this.forumPostsRepository.All().Where(f => f.Title.Contains(text) || f.Content.Contains(text));
        }

        public IQueryable<News> GetNews(string text)
        {
            return this.newsRepository.All().Where(n => n.Title.Contains(text) || n.Content.Contains(text));
        }

        public IQueryable<Specialty> GetSpecialties(string text)
        {
            return this.specialtiesRepository.All().Where(s => s.Name.Contains(text) || s.Description.Contains(text));
        }

        public IQueryable<User> GetTrainers(string text)
        {
            var roleStore = new RoleStore<IdentityRole>();
            var roles = new RoleManager<IdentityRole>(roleStore);
            var trainerRole = roles.FindByName(RoleConstants.Trainer);

            return this.trainersRepository
                .All()
                .Where(u => 
                    u.Roles.Any(r => r.RoleId == trainerRole.Id) && 
                    (u.FirstName.Contains(text) || u.LastName.Contains(text) || u.UserName.Contains(text)));
        }
    }
}
