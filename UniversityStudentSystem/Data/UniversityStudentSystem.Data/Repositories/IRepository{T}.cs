namespace UniversityStudentSystem.Data.Repositories
{
    using System;
    using System.Linq;
    using Models.CommonModels;

    public interface IRepository<T> : IRepository<T, int>
        where T : class, IAuditInfo, IDeletableEntity, IIdentifiableEntity<int>
    {
    }
}
