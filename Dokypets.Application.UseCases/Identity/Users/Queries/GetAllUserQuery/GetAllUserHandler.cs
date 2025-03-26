using AutoMapper;
using Dokypets.Application.Dto;
using Dokypets.Application.Interface.Persistence;
using Dokypets.Application.UseCases.Commons.Bases;
using Dokypets.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Dokypets.Application.UseCases.Identity.Users.Queries.GetAllUserQuery
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, BaseResponse<IEnumerable<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._userManager = userManager;
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<UserDto>>();

            try
            {
                var users = await _unitOfWork.Users.GetAllAsync();

                if (users is not null)
                {
                    response.Data = _mapper.Map<IEnumerable<UserDto>>(users);
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
