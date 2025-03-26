using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Customers.Commands.UpdateCustomerCommand
{
    public class UpdateCustomerCommand: IRequest<BaseResponse<bool>>
    {
        public Guid Id { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
    }
}
