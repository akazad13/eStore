using AutoMapper;
using Exino.Application.CQRS.Material.Commands.CreateMaterial;
using Exino.Application.CQRS.Material.Commands.UpdateMaterial;
using Exino.Domain.Entities;

namespace Exino.Application.Common.Mapper.Profiles
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
