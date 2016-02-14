namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using UniversityStudentSystem.Data.Models;

    public interface IUserService
    {
        int GetNextFacultyNumber();

        IQueryable<User> GetAll();

        IQueryable<IdentityRole> GetRoles();
    }
}
