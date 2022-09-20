using AutoMapper;
using Exino.Application.Common.Mapper;
using Exino.Domain.Entities;

namespace Exino.Application.CQRS.User.Queries.Login
{
    public class UserLoginQueryResponse : IMapFrom<AppUser>
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ImageSource { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Gender { get; set; }
        public string? JWT { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, UserLoginQueryResponse>();
        }
    }
}
