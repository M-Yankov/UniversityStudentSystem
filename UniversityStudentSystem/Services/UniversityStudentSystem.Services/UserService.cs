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

        public int GetNextFacultyNumber()
        {
           return (int)this.usersRepository.All().Select(u => u.FacultyNumber).Max();
        }
    }
}
