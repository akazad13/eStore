using AutoMapper;
using eStore.Application.CQRS.Material.Commands.CreateMaterial;
using eStore.Application.CQRS.Material.Commands.UpdateMaterial;
using eStore.Domain.Entities;

namespace eStore.Application.Common.Mapper.Profiles
{
    public class MaterialProfile : Profile, IOrderedMapperProfile
    {
        #region Ctor
        public MaterialProfile()
        {
            CreateMap<CreateMaterialCommandRequest, Material>();
            CreateMap<UpdateMaterialCommandRequest, Material>();
        }
        #endregion

        public int Order => 1;
    }
}
