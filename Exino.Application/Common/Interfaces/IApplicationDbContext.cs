using Exino.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exino.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<T> GetDbSet<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
