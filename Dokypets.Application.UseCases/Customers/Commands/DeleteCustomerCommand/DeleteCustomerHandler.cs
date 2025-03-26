using AutoMapper;
using Dokypets.Application.Interface.Persistence;
using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Customers.Commands.DeleteCustomerCommand
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<bool>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var customer = await _unitOfWork.Customers.GetAsync(command.Id);

                if (customer == null)
                {
                    response.Message = "User doesn't exist!";

                    return response;
                }

                response.Data = await _unitOfWork.Customers.DeleteAsync(command.Id);

                if (response.Data)
                {
                    response.succcess = true;
                    response.Message = "Delete succeed!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
