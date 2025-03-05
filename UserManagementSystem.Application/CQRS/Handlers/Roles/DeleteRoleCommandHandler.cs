using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagementSystem.Application.CQRS.Commands.Role;
using UserManagementSystem.Infrastructure.Interfaces;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleRepository.DeleteRoleAsync(request.RoleId);
    }
}
