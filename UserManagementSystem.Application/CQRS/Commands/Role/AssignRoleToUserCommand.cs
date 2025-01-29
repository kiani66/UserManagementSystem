using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Application.CQRS.Commands.Role
{
    public class AssignRoleToUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public AssignRoleToUserCommand(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
