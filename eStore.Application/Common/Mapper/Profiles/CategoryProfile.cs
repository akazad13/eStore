using AutoMapper;
using eStore.Application.CQRS.Category.Commands.CreateCategory;
using eStore.Domain.Entities;

namespace eStore.Application.Common.Mapper.Profiles
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
