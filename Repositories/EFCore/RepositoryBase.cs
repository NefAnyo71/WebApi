using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repositories.EFCore
{
    public abstract class RepositoryBase<T> : IrepositoryBase<T>
        where T : class
    {
        protected readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
        }

        public IQueryable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, 
            bool trackChanges)
        {
            return !trackChanges ? 
                _context.Set<T>().Where(expression).AsNoTracking()
                : 
                _context.Set<T>().Where(expression);    
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }                     
    }
}

    