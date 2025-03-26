using FluentValidation;

namespace Dokypets.Application.UseCases.Customers.Commands.FilterWithPaginationCustomerCommand
{
    public class FilterWithPaginationCustomerValidator : AbstractValidator<FilterWithPaginationCustomerCommand>
    {
        public FilterWithPaginationCustomerValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .NotNull()
                .NotEmpty();
        }
    }
}
