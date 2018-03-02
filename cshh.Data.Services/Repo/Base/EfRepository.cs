using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{
    public abstract class EfRepository :
          IRepository, IDisposable
    //where T : class, IBaseEntity
    {
        protected readonly DbContext Context;

        //public List<ICrudChecker> CrudCheckerList { get; set; } //todo ICrudChecker убрать из этого класса.
        protected EfRepository(DbContext context)
        {
            Context = context;
        }

        public virtual IQueryable<T> GetAll<T>() where T : class
        {
            IQueryable<T> query = Context.Set<T>();
            return query;
        }

        public T GetByKey<T>(object key) where T : class//, IBaseEntity
        {
            var query = Context.Set<T>().Find(key);
            return query;
        }
        public object GetByKey(Type type, object key)
        {
            var query = Context.Set(type).Find(key);
            return query;
        }

        public async Task<T> GetByKeyAsync<T>(object key) where T : class//, IBaseEntity
        {
            return await Context.Set<T>().FindAsync(key);
        }


        public IQueryable<T> FindBy<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> query = Context.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add<T>(T entity, bool save = false) where T : class
        {
            Context.Set(entity.GetType()).Add(entity);//dont even ask
            if(save)
                Save();
        }

        public virtual void Delete<T>(T entity, bool save = false) where T : class
        {
            Context.Set<T>().Remove(entity);
            if(save)
                Save();
        }

        public virtual void Update<T>(T entity, bool save = false) where T : class
        {
            var entry = Context.Entry(entity);
            if(entry.State != EntityState.Added)
                entry.State = EntityState.Modified;
            if(save)
                Save();
        }

        public virtual void Save()
        {
            var entries = Context.ChangeTracker.Entries();
            Context.SaveChanges();
        }
        public event EventHandler<object> OnSavingEntity;
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
