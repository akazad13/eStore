using Exino.Application.Common.Interfaces;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Entities;

namespace Exino.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IApplicationDbContext appDbContext) : base(appDbContext) { }
    }
}
