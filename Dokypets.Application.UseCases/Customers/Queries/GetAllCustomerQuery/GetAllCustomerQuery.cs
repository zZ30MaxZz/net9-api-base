using Dokypets.Application.Dto;
using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Customers.Queries.GetAllCustomerQuery
{
    public class GetAllCustomerQuery: IRequest<BaseResponse<IEnumerable<CustomerDto>>>
    {
    }
}
