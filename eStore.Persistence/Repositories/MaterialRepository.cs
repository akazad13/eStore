using eStore.Application.Common.Interfaces;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Entities;

namespace eStore.Persistence.Repositories
{
    internal class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(IApplicationDbContext appDbContext) : base(appDbContext) { }
    }
}
