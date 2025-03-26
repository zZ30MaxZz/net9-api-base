using AutoMapper;
using Dokypets.Application.Dto;
using Dokypets.Application.Interface.Persistence;
using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Customers.Queries.GetByIdCustomerQuery
{
    public class GetByIdCustomerHandler : IRequestHandler<GetByIdCustomerQuery, BaseResponse<CustomerDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<CustomerDto>> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CustomerDto>();
            try
            {
                Guid id = request.Id ?? Guid.Empty;

                if (id == Guid.Empty)
                    throw new Exception("Id customer is null");

                var customer = await _unitOfWork.Customers.GetAsync(id);

                if(customer is not null)
                {
                    response.Data = _mapper.Map<CustomerDto>(customer);
                    response.succcess = true;
                    response.Message = "Query succeed!";
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
