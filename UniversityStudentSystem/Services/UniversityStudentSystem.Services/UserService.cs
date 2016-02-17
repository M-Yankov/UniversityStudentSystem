namespace UniversityStudentSystem.Services
{
    using System;
    using System.Collections.Generic;
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
        private IRepository<Candidate> candidatesRepository;

        public UserService(IRepository<User, string> users, IRepository<Candidate> candidatures)
        {
            this.usersRepository = users;
            this.candidatesRepository = candidatures;
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

        public IQueryable<Candidate> GetCandidatures(string id)
        {
           return this.candidatesRepository.All().Where(c => c.UserId == id);
        }

        public bool CanApply(string id)
        {
            var candidature = this.candidatesRepository.All()
                .Where(c => c.UserId == id)
                .OrderByDescending(c => c.DateSent).FirstOrDefault();
            var user = this.usersRepository.GetById(id);
            
            if ((candidature == null || candidature.IsRejected) && user.Status != Status.Confirmed)
            {
                return true;
            }

            return false;
        }

        public void MakeApply(string userId, int specialtyId, string path)
        {
            Candidate candidate = new Candidate()
            {
                UserId = userId,
                SpecialtyId = specialtyId,
                DateSent = DateTime.Now,
                IsApproved = false,
                IsRejected = false,
                Documents = new HashSet<Document>()
                {
                    new Document()
                    {
                        DateUploaded = DateTime.Now,
                        Path = path,
                        UserId = userId 
                    }
                }
            };

            this.candidatesRepository.Add(candidate);
            this.candidatesRepository.Save();
        }
    }
}
