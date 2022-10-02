using Exino.Application.Common.Interfaces;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Entities;

namespace Exino.Persistence.Repositories
{
    internal class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IApplicationDbContext appDbContext) : base(appDbContext) { }
    }
}
