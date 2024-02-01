using eStore.Application.Common.Interfaces;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Entities;

namespace eStore.Persistence.Repositories
{
    public class CategoryRepository(IApplicationDbContext appDbContext) : BaseRepository<Category>(appDbContext), ICategoryRepository
    {
    }
}
