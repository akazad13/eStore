using AutoMapper;
using Exino.Application.CQRS.Product.Commands.CreateProduct;
using Exino.Domain.Entities;

namespace Exino.Application.Common.Mapper.Profiles
{
    public class ProductProfile : Profile, IOrderedMapperProfile
    {
        #region Ctor
        public ProductProfile()
        {
            CreateMap<CreateProductCommandRequest, Product>();
        }
        #endregion

        public int Order => 1;
    }
}
