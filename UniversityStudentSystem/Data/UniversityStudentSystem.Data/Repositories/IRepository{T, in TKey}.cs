using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityStudentSystem.Data.Models.CommonModels;

namespace UniversityStudentSystem.Data.Repositories
{
    public interface IRepository <T, TKey>
        where T : class, IAuditInfo, IDeletableEntity, IIdentifiableEntity<TKey>
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(TKey id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();
    }
}
