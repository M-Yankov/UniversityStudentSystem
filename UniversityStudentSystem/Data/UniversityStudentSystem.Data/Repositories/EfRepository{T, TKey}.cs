namespace UniversityStudentSystem.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models.CommonModels;

    public class EfRepository<T, TKey> : IRepository<T, TKey>
        where T : class, IAuditInfo, IDeletableEntity, IIdentifiableEntity<TKey> 
    {
        public EfRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException(
                    "An instance of DbContext is required to use this repository.", 
                    nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private IDbSet<T> DbSet { get; }

        private DbContext Context { get; }

        public IQueryable<T> All()
        {
            return this.DbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.DbSet;
        }

        public T GetById(TKey id)
        {
            T entity = this.DbSet.Find(id);
            return entity != null && !entity.IsDeleted ? entity : null;
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
        }

        public void HardDelete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }
    }
}
