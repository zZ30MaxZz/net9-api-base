using Dokypets.Application.Dto;
using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Identity.Users.Commands.LoginCommand
{
    public class LoginCommand : IRequest<BaseResponse<SignInResultDto>>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
