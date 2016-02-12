using System.Data.Entity;
using UniversityStudentSystem.Data.Models.CommonModels;

namespace UniversityStudentSystem.Data.Repositories
{
    public class EfRepository<T> : EfRepository<T, int>, IRepository<T>
        where T : class, IAuditInfo, IDeletableEntity, IIdentifiableEntity<int>
    {
        public EfRepository(DbContext context)
            :base(context)
        {

        }
    }
}
