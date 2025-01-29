using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Application.CQRS.Commands.Role
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public int RoleId { get; set; }

        public DeleteRoleCommand(int roleId)
        {
            RoleId = roleId;
        }
    }
}
