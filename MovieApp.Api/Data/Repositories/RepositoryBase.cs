using Microsoft.EntityFrameworkCore;
using MovieApp.Api.Data.Interfaces;
using System.Linq.Expressions;

namespace MovieApp.Api.Data.Repositories
{
    /// <summary>
    /// Base class for repositories with standard CRUD operations tied to a single entity of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MovieContext RepositoryContext { get; set; }

        public RepositoryBase(MovieContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
            this.RepositoryContext.SaveChanges();
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
            this.RepositoryContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
            this.RepositoryContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            //Implement at the Repository level
            throw new Exception("Not implemented in repository base");
        }
    }
}
