

using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using PJira.Application.DTOs;

namespace PJira.Application.Common.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<IdentityUser, UserDto>().ReverseMap()
                .ForMember(p => p.PasswordHash, m => m.MapFrom(p => p.Password));

        }
    }
}
