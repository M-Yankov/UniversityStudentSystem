﻿namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using UniversityStudentSystem.Data.Models;

    public interface IUserService
    {
        long GetLastFacultyNumber();

        IQueryable<User> GetAll();

        IQueryable<IdentityRole> GetRoles();

        User GetById(string id);

        void Update(User user);

        void ClearAvatar(string id);
    }
}
