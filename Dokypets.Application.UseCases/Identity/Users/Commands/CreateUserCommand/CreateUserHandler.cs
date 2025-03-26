using AutoMapper;
using Dokypets.Application.Interface.Persistence;
using Dokypets.Application.UseCases.Commons.Bases;
using Dokypets.Common.Enums;
using Dokypets.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Dokypets.Application.UseCases.Identity.Users.Commands.CreateUserCommand
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._userManager = userManager;
        }

        public async Task<BaseResponse<bool>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var emailUser = await _userManager.FindByEmailAsync(command.Email);

                if (emailUser != null)
                {
                    response.Message = "Email registered";
                    return response;
                }

                var userNameUser = await _userManager.FindByNameAsync(command.UserName);

                if (userNameUser != null)
                {
                    response.Message = "Username registered";
                    return response;
                }

                var user = new ApplicationUser
                {
                    UserName = command.UserName,
                    Email = command.Email,
                    Address = command.Address ?? string.Empty,
                    UrlPhoto = command.UrlPhoto ?? string.Empty,
                    FirstName = command.UserName ?? "Default",
                    LastName = command.LastName ?? "Default",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };

                var responseIdentity = await _userManager.CreateAsync(user, command.Password);

                response.Data = responseIdentity.Succeeded;

                if (response.Data)
                {
                    await _userManager.AddToRoleAsync(user, Roles.User.ToString());

                    response.succcess = true;
                    response.Message = "Create succeed!";
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
