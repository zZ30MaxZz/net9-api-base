using FluentValidation;

namespace Dokypets.Application.UseCases.Identity.Users.Commands.CreateUserCommand
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password is required")
               .MinimumLength(6).WithMessage("Password must be at least 6 characters")
               .Must(HasUpperCase).WithMessage("Password must contain at least one uppercase letter")
               .Must(HasLowerCase).WithMessage("Password must contain at least one lowercase letter")
               .Must(HasDigit).WithMessage("Password must contain at least one number")
               .Must(HasNonAlphanumeric).WithMessage("Password must contain at least one special character")
               .Must(HasUniqueChars).WithMessage("Password must contain at least one unique character");
        }

        private bool HasUpperCase(string password)
        {
            return password.Any(char.IsUpper);
        }

        private bool HasLowerCase(string password)
        {
            return password.Any(char.IsLower);
        }

        private bool HasDigit(string password)
        {
            return password.Any(char.IsDigit);
        }

        private bool HasUniqueChars(string password)
        {
            return password.Distinct().Count() >= 1;
        }

        private bool HasNonAlphanumeric(string password)
        {
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
}
