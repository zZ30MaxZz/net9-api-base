using AutoMapper;
using Dokypets.Application.Dto;
using Dokypets.Application.UseCases.Identity.Users.Commands.CreateUserCommand;
using Dokypets.Domain.Entities.Identity;

namespace Dokypets.Application.UseCases.Commons.Mappings.Identity
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<ApplicationUser, CreateUserCommand>().ReverseMap();
        }
    }
}
