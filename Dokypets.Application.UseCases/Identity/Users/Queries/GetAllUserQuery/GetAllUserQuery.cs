using Dokypets.Application.Dto;
using Dokypets.Application.UseCases.Commons.Bases;
using MediatR;

namespace Dokypets.Application.UseCases.Identity.Users.Queries.GetAllUserQuery
{
    public class GetAllUserQuery : IRequest<BaseResponse<IEnumerable<UserDto>>>
    {
    }
}
