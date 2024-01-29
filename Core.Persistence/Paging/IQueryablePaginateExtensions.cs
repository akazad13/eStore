using eStore.Domain.Paging;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Paging
{
    public static class IQueryablePaginateExtensions
    {
        public static async Task<IPaginate<T>> ToPaginateAsync<T>(
            this IQueryable<T> source,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default
        )
        {
            var count = await source.CountAsync();
            List<T> item;
            if (pageSize == -1)
            {
                item = await source.ToListAsync();
                pageSize = count;
            }
            else
            {
                item = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return new Paginate<T>(item, count, pageNumber, pageSize);
        }
    }
}
