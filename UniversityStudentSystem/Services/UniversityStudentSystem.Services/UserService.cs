
namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data.Repositories;
    using UniversityStudentSystem.Data.Models;

    public class UserService : IUserService
    {
        private IRepository<User, string> usersRepository;

        public UserService(IRepository<User, string> repository)
        {
            this.usersRepository = repository;  
        }

        public int GetNextFacultyNumber()
        {
           return (int)this.usersRepository.All().Select(u => u.FacultyNumber).Max();
        }
    }
}
