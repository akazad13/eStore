using AutoMapper;
using Exino.Application.CQRS.Category.Commands.CreateProduct;
using Exino.Domain.Entities;

namespace Exino.Application.Common.Mapper.Profiles
{
    public class CategoryProfile : Profile, IOrderedMapperProfile
    {
        public CategoryProfile()
        {
            this.CreateMap<CreateCategoryCommandRequest, Category>();
        }
        public int Order => 1;
    }
}
