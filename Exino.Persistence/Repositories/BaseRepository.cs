using Core.Persistence.Paging;
using Exino.Application.Common.Interfaces;
using Exino.Application.RepositoriesInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Exino.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IApplicationDbContext _appDbContext;
        private readonly DbSet<T> table;

        public BaseRepository(IApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            table = _appDbContext.GetDbSet<T>();
        }

        public async Task<bool> Commit(CancellationToken cancellationToken)
        {
            return await _appDbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await table.AnyAsync(expression);
        }

        public async Task Create(T entity)
        {
            await table.AddAsync(entity);
        }

        public async Task Create(List<T> entity)
        {
            await table.AddRangeAsync(entity);
        }

        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public async Task<T?> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await table.FirstOrDefaultAsync(expression);
        }

        public async Task<TResult?> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracing = true
        )
        {
            IQueryable<T> query = table;

            if (disableTracing)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (expression != null)
                query = query.Where(expression);

            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<IPaginate<TResult>> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int pageNumber = 1,
            int pageSize = 10,
            bool disableTracing = true,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<T> queryable = table;
            if (disableTracing)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (expression != null)
                queryable = queryable.Where(expression);

            if (orderBy != null)
                return await orderBy(queryable)
                    .Select(selector)
                    .ToPaginateAsync(pageNumber, pageSize, cancellationToken);
            else
                return await queryable
                    .Select(selector)
                    .ToPaginateAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<IPaginate<T>> GetListAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int pageNumber = 1,
            int pageSize = 10,
            bool disableTracing = true,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<T> queryable = table;
            if (disableTracing)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (predicate != null)
                queryable = queryable.Where(predicate);
            if (orderBy != null)
                return await orderBy(queryable)
                    .ToPaginateAsync(pageNumber, pageSize, cancellationToken);
            return await queryable.ToPaginateAsync(pageNumber, pageSize, cancellationToken);
        }

        public void Update(T entity)
        {
            table.Update(entity).State = EntityState.Modified;
        }
    }
}
