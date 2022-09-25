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

        public async Task<TResult?> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
                                                                      Expression<Func<T, bool>>? expression = null,
                                                                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                                                      bool disableTracing = true)
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

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracing = true, int pageIndex = 1, int pageSize = 3)
        {
            IQueryable<T> query = table;

            if (disableTracing)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (expression != null)
                query = query.Where(expression);

            if (orderBy != null)
                return await orderBy(query).Select(selector).Skip((pageIndex - 1) * pageSize).ToListAsync();
            else
                return await query.Select(selector).Skip((pageIndex - 1) * pageSize).ToListAsync();
        }

        public void Update(T entity)
        {
            table.Update(entity).State = EntityState.Modified;
        }
    }
}
