using System.Linq.Expressions;

namespace MovieApp.Api.Data.Interfaces
{
    /// <summary>
    /// Repository Base Interface with CRUD operations for a given entity of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Exists(int id);
    }
}
