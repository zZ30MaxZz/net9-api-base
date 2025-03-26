using AutoMapper;
using Dokypets.Application.Interface.Persistence;
using Dokypets.Application.UseCases.Commons.Bases;
using Dokypets.Domain.Entities;
using MediatR;

namespace Dokypets.Application.UseCases.Customers.Commands.UpdateCustomerCommand
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<bool>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var customerExist = await _unitOfWork.Customers.GetAsync(command.Id);

                if (customerExist == null)
                {
                    response.Message = "Customer doesn't exist!";

                    return response;
                }

                var customer = _mapper.Map<Customer>(command);
                response.Data = await _unitOfWork.Customers.UpdateAsync(customer);

                if (response.Data)
                {
                    response.succcess = true;
                    response.Message = "Update succeed!";
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
