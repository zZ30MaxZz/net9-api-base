using FluentValidation;

namespace Dokypets.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerValidator: AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.ContactName).NotEmpty().NotNull();
            RuleFor(x => x.Phone).NotEmpty().NotNull();
            RuleFor(x => x.Region).NotEmpty().NotNull();
        }
    }
}
