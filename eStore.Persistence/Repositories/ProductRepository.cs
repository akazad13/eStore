using eStore.Application.Common.Interfaces;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Entities;

namespace eStore.Persistence.Repositories
{
    internal class ProductRepository(IApplicationDbContext appDbContext) : BaseRepository<Product>(appDbContext), IProductRepository
    {
    }
}
