using MediatR;
using UserManagementSystem.Application.CQRS.Commands.Role;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Interfaces;

namespace UserManagementSystem.Application.CQRS.Handlers.Roles
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role { Name = request.Name };

            // اضافه کردن مجوزها
            role.RolePermissions = request.PermissionIds.Select(id => new RolePermission
            {
                PermissionId = id
            }).ToList();

            await _roleRepository.AddRoleAsync(role);
            return role.Id;
        }
    }
}
