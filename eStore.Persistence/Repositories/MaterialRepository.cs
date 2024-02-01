using eStore.Application.Common.Interfaces;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Entities;

namespace eStore.Persistence.Repositories
{
    internal class MaterialRepository(IApplicationDbContext appDbContext) : BaseRepository<Material>(appDbContext), IMaterialRepository
    {
    }
}
