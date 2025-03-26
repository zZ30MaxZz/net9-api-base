using Dokypets.Application.Dto;
using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Customers.Queries.GetByIdCustomerQuery
{
    public class GetByIdCustomerQuery: IRequest<BaseResponse<CustomerDto>>
    {
        public Guid? Id { get; set; }
    }
}
