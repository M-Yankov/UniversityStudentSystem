namespace UniversityStudentSystem.Services.Contracts
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

        IQueryable<Candidate> GetCandidatures(string id);

        bool CanApply(string id);

        void MakeApply(string userId, int specialtyId, string path);
    }
}
