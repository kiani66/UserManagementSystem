using MediatR;
using UserManagementSystem.Application.CQRS.Queries.Role;
using UserManagementSystem.Infrastructure.Interfaces;
using UserManagementSystem.Domain.Entities;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Role>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _roleRepository.GetRoleByIdAsync(request.RoleId);
    }
}
