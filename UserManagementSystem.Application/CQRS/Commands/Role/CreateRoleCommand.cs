using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Application.CQRS.Commands.Role
{
    public class CreateRoleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public List<int> PermissionIds { get; set; } // مجوزهایی که به این نقش تعلق دارند

        public CreateRoleCommand(string name, List<int> permissionIds)
        {
            Name = name;
            PermissionIds = permissionIds;
        }
    }
}
