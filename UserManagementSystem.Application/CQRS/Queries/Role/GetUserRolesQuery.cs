using MediatR;
namespace UserManagementSystem.Application.CQRS.Queries.Role
{
    public class GetUserRolesQuery : IRequest<List<string>>
    {
        public int UserId { get; set; }

        public GetUserRolesQuery(int userId)
        {
            UserId = userId;
        }
    }
}
