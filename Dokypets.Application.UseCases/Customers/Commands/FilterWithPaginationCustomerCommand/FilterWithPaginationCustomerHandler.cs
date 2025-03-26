using AutoMapper;
using Dokypets.Application.Dto;
using Dokypets.Application.Interface.Persistence;
using Dokypets.Application.UseCases.Commons.Bases;
using Dokypets.Domain.Entities;
using MediatR;

namespace Dokypets.Application.UseCases.Customers.Commands.FilterWithPaginationCustomerCommand
{
    internal class FilterWithPaginationCustomerHandler : IRequestHandler<FilterWithPaginationCustomerCommand, BaseResponsePagination<IEnumerable<CustomerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FilterWithPaginationCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponsePagination<IEnumerable<CustomerDto>>> Handle(FilterWithPaginationCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponsePagination<IEnumerable<CustomerDto>>();
            try
            {
                var (customers, totalRecords) = await _unitOfWork.Customers.GetAllWithPaginationAsync(
                    new FilterCustomer
                    {
                        PageNumber = request.PageNumber,
                        PageSize = request.PageSize,
                        CompanyName = request.CompanyName,
                        ContactName = request.ContactName,
                        ContactTitle = request.ContactTitle
                    });

                if (customers is not null)
                {
                    response.PageNumber = request.PageNumber;
                    response.TotalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize);
                    response.TotalCount = totalRecords;
                    response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                    response.succcess = true;
                    response.Message = "Succeed!";
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
