using Microsoft.EntityFrameworkCore;
using Repository;
using System.Linq.Expressions;

namespace Contracts
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext repositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public async Task<T> Create(T entity)
        {
            await repositoryContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task<bool> Delete(T entity)
        {
            repositoryContext.Set<T>().Remove(entity);
            return Task.FromResult(true);
        }

        public Task<T> Update(T entity)
        {
            repositoryContext.Set<T>().Update(entity);
            return Task.FromResult(entity);
        }

        public IQueryable<T> FindAll()
        {
            return repositoryContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return repositoryContext.Set<T>().Where(expression);
        }
    }
}
