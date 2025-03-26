using FluentValidation;

namespace Dokypets.Application.UseCases.Identity.Users.Commands.LoginCommand
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
