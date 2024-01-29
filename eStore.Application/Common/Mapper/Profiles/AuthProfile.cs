using AutoMapper;
using eStore.Application.Common.Mapper;
using eStore.Application.CQRS.Authentication.Queries.Login;
using eStore.Domain.Entities;

namespace HRMS.Infrastructure.Mapper.Profiles
{
    public class AuthProfile : Profile, IOrderedMapperProfile
    {
        #region Ctor
        public AuthProfile()
        {
            CreateMap<AppUser, UserLoginQueryResponse>();
        }
        #endregion

        public int Order => 1;
    }
}
