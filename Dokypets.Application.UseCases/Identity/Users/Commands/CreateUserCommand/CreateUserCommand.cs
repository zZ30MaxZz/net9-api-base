using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Identity.Users.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<BaseResponse<bool>>
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? UrlPhoto { get; set; }
        public string? LastName { get; internal set; }
    }
}
