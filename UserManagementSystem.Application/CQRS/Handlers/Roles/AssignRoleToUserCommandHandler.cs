using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Application.CQRS.Commands.Role;
using UserManagementSystem.Infrastructure.Interfaces;

namespace UserManagementSystem.Application.CQRS.Handlers.Roles
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public AssignRoleToUserCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            return await _roleRepository.AssignRoleToUserAsync(request.UserId, request.RoleId);
        }
    }
}
