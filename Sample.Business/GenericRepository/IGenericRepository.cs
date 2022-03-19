using Sample.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.GenericRepository
{
    public interface IGenericRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        T Get(int id);
        T Get(Expression<Func<T, bool>> expression);
        IQueryable<T> GetTake(Expression<Func<T, bool>> expression, int count);
        void Delete(int id);
        void Delete(T entity);
        void DeleteRange(List<T> entites);
        void Update(T entity);
        void UpdateRange(List<T> entites);
        void Add(T entity);
        void AddRange(List<T> entites);
        SampleDbContext GetQueryDbContext();
    }
}
