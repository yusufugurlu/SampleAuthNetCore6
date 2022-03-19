using Microsoft.EntityFrameworkCore;
using Sample.DataAccess;
using Sample.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly SampleDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(SampleDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreateDate = DateTime.Now;
            }
        }

        public void AddRange(List<T> entites)
        {
            _dbSet.AddRange(entites);
            foreach (var entity in entites)
            {
                if (entity is BaseEntity baseEntity)
                {
                    baseEntity.CreateDate = DateTime.Now;
                }

            }
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                if (entity is BaseEntity baseEntity)
                {
                    baseEntity.IsDisabled = true;
                    _dbSet.Update(entity);
                }
                else
                {
                    // if not baseEntity is remove
                    _dbSet.Remove(entity);
                }
            }
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                if (entity is BaseEntity baseEntity)
                {
                    baseEntity.IsDisabled = true;
                    _dbSet.Update(entity);
                }
                else
                {
                    // if not baseEntity is remove
                    _dbSet.Remove(entity);
                }
            }
        }

        public void DeleteRange(List<T> entites)
        {
            foreach (var entity in entites)
            {
                if (entity != null)
                {
                    if (entity is BaseEntity baseEntity)
                    {
                        baseEntity.IsDisabled = true;
                        _dbSet.Update(entity);
                    }
                    else
                    {
                        // if not baseEntity is remove
                        _dbSet.Remove(entity);
                    }
                }
            }
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking().FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        public SampleDbContext GetQueryDbContext()
        {
            return _context;
        }

        public IQueryable<T> GetTake(Expression<Func<T, bool>> expression, int count)
        {
            return _dbSet.Where(expression).AsNoTracking().Take(count);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(List<T> entites)
        {
            _dbSet.UpdateRange(entites);
        }
    }
}
