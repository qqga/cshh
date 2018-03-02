using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{

    public delegate IQueryable<T> IncludeFunc<T>(IQueryable<T> inQuery);

    public interface IRepository : IDisposable //where T : IBaseEntity
    {
        IQueryable<T> GetAll<T>() where T : class;
        T GetByKey<T>(object key) where T : class;
        object GetByKey(Type type, object key);
        Task<T> GetByKeyAsync<T>(object key) where T : class;
        IQueryable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Add<T>(T entity, bool save = false) where T : class;
        void Delete<T>(T entity, bool save = false) where T : class;
        void Update<T>(T entity, bool save = false) where T : class;
        event EventHandler<object> OnSavingEntity;
        void Save();
    }

    public static class IRepositoryExtenxions
    {
        public static void Delete<T>(this IRepository rep, object id, bool save = false) where T : class
        {
            var entry = rep.GetByKey<T>(id);
            if(entry == null)
                throw new InvalidOperationException($"в БД не найдена запись '{typeof(T).Name}' с ключем {id}");

            rep.Delete(entry, save);
        }

        public static List<T> GetList<T>(this IRepository rep) where T : class
        {
            return rep.GetAll<T>().ToList();
        }

        public static Task<List<T>> GetListAsync<T>(this IRepository rep) where T : class
        {
            return rep.GetAll<T>().ToListAsync();

        }

        public static T GetByKey<T>(this IRepository rep, object key, Func<T, object> keySelector, IncludeFunc<T> includeFunk) where T : class //: BaseEntity
        {
            return includeFunk(rep.GetAll<T>()).Where(o => keySelector(o) == key).FirstOrDefault();
        }
    }
}
