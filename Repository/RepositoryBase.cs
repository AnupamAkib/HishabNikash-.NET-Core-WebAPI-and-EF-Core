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

        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);

        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);

        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);

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
