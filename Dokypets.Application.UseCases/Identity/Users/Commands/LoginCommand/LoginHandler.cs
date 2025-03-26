using AutoMapper;
using Dokypets.Application.Dto;
using Dokypets.Application.Interface.Persistence;
using Dokypets.Application.UseCases.Commons.Bases;
using Dokypets.Domain.Entities.Identity;
using Dokypets.Domain.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dokypets.Application.UseCases.Identity.Users.Commands.LoginCommand
{
    public class LoginHandler : IRequestHandler<LoginCommand, BaseResponse<SignInResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _Jwt;

        public LoginHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IOptions<JWT> jwt)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._signInManager = signInManager;
            this._userManager = userManager;
            _Jwt = jwt.Value;
        }

        public async Task<BaseResponse<SignInResultDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<SignInResultDto>();

            try
            {
                var data = await _signInManager.PasswordSignInAsync(
                    command.Username,
                    command.Password,
                    isPersistent: false,
                    lockoutOnFailure: true);

                response.Data = _mapper.Map<SignInResultDto>(data);

                if (data.Succeeded)
                {
                    var claims = new List<Claim>();

                    var user = await _userManager.FindByNameAsync(command.Username);
                    var roles = await _userManager.GetRolesAsync(user!);

                    var jwtSecurityToken = await CreateJwtAsync(user);

                    response.Data.Email = user.Email;
                    response.Data.Roles = roles.ToList();
                    response.Data.UserName = user.UserName;
                    response.Data.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    response.Data.TokenExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime();
                    response.Data.RefreshToken = Guid.NewGuid();

                    response.succcess = true;
                    response.Message = "Logged!";
                }
                else if (data.IsLockedOut)
                {
                    response.Message = "Account is locked. Please try again later.";
                }
                else if (data.RequiresTwoFactor)
                {
                    response.Message = "Two factor authentication is required.";
                }
                else
                {
                    response.Message = "Email or Password failed!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        #region
        private async Task<JwtSecurityToken> CreateJwtAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            //generate the symmetricSecurityKey by the s.key
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Key));

            //generate the signingCredentials by symmetricSecurityKey
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //define the  values that will be used to create JWT
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _Jwt.Issuer,
                audience: _Jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_Jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        #endregion
    }
}