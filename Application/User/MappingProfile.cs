using System.Linq;
using AutoMapper;
using Domain.Models;

namespace Application.User
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Register.Command, AppUser>();
            CreateMap<AppUser, UserDetailDto>()
                .ForMember(u => u.FullName, o => o.MapFrom(s => ($"{s.LastName}, {s.FirstName} {s.MiddleName}").TrimEnd()))
                .ForMember(u => u.Role, o => o.MapFrom(s => s.UserRoles.Any() ? s.UserRoles.SingleOrDefault().Role.Name : "Member"));
        }
    }
}