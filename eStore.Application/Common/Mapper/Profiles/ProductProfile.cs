using AutoMapper;
using eStore.Application.CQRS.Product.Commands.CreateProduct;
using eStore.Domain.Entities;

namespace eStore.Application.Common.Mapper.Profiles
{
    public class ProductProfile : Profile, IOrderedMapperProfile
    {
        #region Ctor
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();
        }
        #endregion

        public int Order => 1;
    }
}
