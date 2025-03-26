using Dokypets.Application.UseCases.Identity.Users.Commands.CreateUserCommand;
using Dokypets.Application.UseCases.Identity.Users.Commands.LoginCommand;
using Dokypets.Application.UseCases.Identity.Users.Queries.GetAllUserQuery;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dokypets.Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);

            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllUserQuery());
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] CreateUserCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);

            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
