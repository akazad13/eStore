using AutoMapper;
using Exino.Application.Common.Mapper;
using Exino.Application.CQRS.Authentication.Queries.Login;
using Exino.Domain.Entities;

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
