using MediatR;
namespace UserManagementSystem.Application.CQRS.Queries.Role
{
    public class GetAllRolesQuery : IRequest<List<UserManagementSystem.Domain.Entities.Role>> 
    {

    }
}
