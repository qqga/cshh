using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{

    //public delegate IQueryable<T> IncludeFunc<T>(IQueryable<T> inQuery);

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
        public static void SetProxyCreationEnabled(this IRepository rep, bool val)
        {            
            ((EfRepository)rep).Context.Configuration.ProxyCreationEnabled = val;//todo
        }

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

        public static T GetByKey<T>(this IRepository rep, int key,params string[] include) where T : BaseEntity
        {
            IQueryable<T> all = rep.GetAll<T>();

            foreach(string inc in include)
            {
                all = all.Include(inc);
            }

            return all.Where(o => o.Id == key).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rep"></param>
        /// <param name="key"></param>
        /// <param name="includePath">include</param>
        /// <returns></returns>
        public static T GetByKey<T>(this IRepository rep, int key,params Expression<Func<T, object>>[] includePath) where T : BaseEntity //todo check object Expression or back to includefunc
        {
            IQueryable<T> all = rep.GetAll<T>();

            foreach(Expression<Func<T, object>> include in includePath)
            {
                all = all.IncludeQ(include);
            }
            return all.Where(o => o.Id == key).FirstOrDefault();
        }

        //public static T GetByKey<T>(this IRepository rep, int key, IncludeFunc<T> includeFunk) where T : BaseEntity
        //{
        //    return includeFunk(rep.GetAll<T>()).Where(o => o.Id == key).FirstOrDefault();
        //}

        //public static T GetByKey<T>(this IRepository rep, object key, Func<T, object> keySelector, IncludeFunc<T> includeFunk) where T : class //: BaseEntity
        //{
        //    return includeFunk(rep.GetAll<T>()).Where(o => keySelector(o) == key).FirstOrDefault();
        //}


        public static IQueryable<T> IncludeQ<T>(this IQueryable<T> source, string path)=> QueryableExtensions.Include(source,path);        
        public static IQueryable IncludeQ(this IQueryable source, string path)=> QueryableExtensions.Include(source,path);        
        public static IQueryable<T> IncludeQ<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> path)=> QueryableExtensions.Include(source,path);
    }
}
