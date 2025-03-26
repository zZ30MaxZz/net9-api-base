using AutoMapper;
using Dokypets.Application.Dto;
using Dokypets.Application.UseCases.Identity.Users.Commands.LoginCommand;
using Microsoft.AspNetCore.Identity;

namespace Dokypets.Application.UseCases.Commons.Mappings.Identity
{
    public class SignInMapper : Profile
    {
        public SignInMapper()
        {
            CreateMap<SignInResult, SignInResultDto>().ReverseMap();
            CreateMap<SignInResult, LoginCommand>().ReverseMap();
        }
    }
}
