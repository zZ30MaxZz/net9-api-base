using Dokypets.Application.Dto;
using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Customers.Commands.FilterWithPaginationCustomerCommand
{
    public class FilterWithPaginationCustomerCommand : IRequest<BaseResponsePagination<IEnumerable<CustomerDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
    }
}
