using Microsoft.EntityFrameworkCore;

namespace eStore.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<T> GetDbSet<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
