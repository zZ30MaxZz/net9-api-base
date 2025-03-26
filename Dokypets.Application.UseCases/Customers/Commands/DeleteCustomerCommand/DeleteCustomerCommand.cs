using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Customers.Commands.DeleteCustomerCommand
{
    public class DeleteCustomerCommand: IRequest<BaseResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
