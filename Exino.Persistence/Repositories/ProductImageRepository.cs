using Exino.Application.Common.Interfaces;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Entities;

namespace Exino.Persistence.Repositories
{
    public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(IApplicationDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
