namespace UniversityStudentSystem.Data.Repositories
{
    using System.Data.Entity;
    using UniversityStudentSystem.Data.Models.CommonModels;

    public class EfRepository<T> : EfRepository<T, int>, IRepository<T>
        where T : class, IAuditInfo, IDeletableEntity, IIdentifiableEntity<int>
    {
        public EfRepository(DbContext context)
            : base(context)
        {
        }
    }
}
