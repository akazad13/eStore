using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace eStore.Application.RepositoriesInterface
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        Task Create(List<T> entity);
        void Update(T entity);
        Task<bool> Commit(CancellationToken cancellationToken);

        void Delete(T entity);
        Task<T?> GetDefault(Expression<Func<T, bool>> expression);

        Task<bool> Any(Expression<Func<T, bool>> expression);
        Task<TResult?> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool dissableTracing = true
        );

        Task<IPaginate<TResult>> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int pageNumber = 1,
            int pageSize = 10,
            bool disableTracing = true,
            CancellationToken cancellationToken = default
        );
        Task<IPaginate<T>> GetListAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int pageNumber = 1,
            int pageSize = 10,
            bool disableTracing = true,
            CancellationToken cancellationToken = default
        );
    }
}
