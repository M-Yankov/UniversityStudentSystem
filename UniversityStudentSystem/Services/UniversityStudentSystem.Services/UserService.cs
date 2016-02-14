namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Common;
    using Contracts;
    using Data.Repositories;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using UniversityStudentSystem.Data.Models;

    public class UserService : IUserService
    {
        private IRepository<User, string> usersRepository;

        public UserService(IRepository<User, string> repository)
        {
            this.usersRepository = repository;
        }

        public IQueryable<IdentityRole> GetRoles()
        {
            var roleStore = new RoleStore<IdentityRole>();
            var roles = new RoleManager<IdentityRole>(roleStore);

            return roles.Roles;
        }

        public IQueryable<User> GetAll()
        {
            return usersRepository.All();
        }

        public long GetLastFacultyNumber()
        {
            return this.usersRepository.All().Select(u => u.FacultyNumber).Max();
        }

        public User GetById(string id)
        {
            return this.usersRepository.GetById(id);
        }

        public void Update(User user)
        {
            this.usersRepository.Update(user);
            this.usersRepository.Save();
        }

        public void ClearAvatar(string id)
        {
            User user = this.GetById(id);
            user.AvaratUrl = null;
            this.Update(user);
        }
    }
}
