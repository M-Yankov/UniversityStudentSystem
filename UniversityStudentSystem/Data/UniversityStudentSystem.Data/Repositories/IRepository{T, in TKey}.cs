namespace UniversityStudentSystem.Data.Repositories
{
    using System.Linq;
    using UniversityStudentSystem.Data.Models.CommonModels;

    public interface IRepository<T, TKey>
        where T : class, IAuditInfo, IDeletableEntity, IIdentifiableEntity<TKey>
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(TKey id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();
    }
}
