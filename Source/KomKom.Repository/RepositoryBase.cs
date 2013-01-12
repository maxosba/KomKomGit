using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace KomKom.Repository
{
    public class RepositoryBase<C> : IDisposable where C : DbContext, new()
    {
        private C _dataContext;

        public virtual C DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new C();
                    this.AllowSerialization = true;
                }
                return _dataContext;
            }
        }
        public virtual T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate != null)
            {
                using (DataContext)
                {
                    return DataContext.Set<T>().Where(predicate).SingleOrDefault();
                }
            }
            else
            {
                throw new ApplicationException("Predicate value must be passed to Get<T>.");
            }
        }
        public virtual IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return DataContext.Set<T>().Where(predicate);
            }
            catch (Exception)
            {
                //Log error
            }
            return null;
        }
        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                return GetList(predicate).OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }
        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                return GetList<T>().OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }
        public virtual IQueryable<T> GetList<T>() where T : class
        {
            try
            {
                return DataContext.Set<T>();
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }
        public virtual bool AllowSerialization
        {
            get
            {
                return _dataContext.Configuration.ProxyCreationEnabled;
            }
            set
            {
                _dataContext.Configuration.ProxyCreationEnabled = !value;
            }
        }
        public void Dispose()
        {
            if (DataContext != null) DataContext.Dispose();
        }
    }
}
