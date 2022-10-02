using Exino.Application.Common.Interfaces;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Entities;

namespace Exino.Persistence.Repositories
{
    internal class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(IApplicationDbContext appDbContext) : base(appDbContext) { }
    }
}
