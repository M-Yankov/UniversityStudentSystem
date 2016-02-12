namespace UniversityStudentSystem.Data
{
    using System;
    using System.Linq;
    using Models.CommonModels;
    using Repositories;

    public interface IRepository<T> : IRepository<T, int>
        where T : class, IAuditInfo, IDeletableEntity, IIdentifiableEntity<int>
    {
    }
}
