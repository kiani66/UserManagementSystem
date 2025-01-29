using MediatR;

namespace UserManagementSystem.Application.CQRS.Queries.Role
{
    public class GetRoleByIdQuery : IRequest<UserManagementSystem.Domain.Entities.Role>
    {
        public int RoleId { get; set; }

        public GetRoleByIdQuery(int roleId)
        {
            RoleId = roleId;
        }
    }
}
