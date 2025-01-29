using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Application.CQRS.Commands.Role;
using UserManagementSystem.Application.CQRS.Queries.Role;
using UserManagementSystem.Api.Models;

namespace UserManagementSystem.Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            var roleId = await _mediator.Send(new CreateRoleCommand(request.Name, request.PermissionIds));
            return Ok(new { RoleId = roleId, Message = "Role created successfully!" });
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleRequest request)
        {
            var result = await _mediator.Send(new AssignRoleToUserCommand(request.UserId, request.RoleId));
            if (!result) return BadRequest("Could not assign role.");

            return Ok(new { Message = "Role assigned successfully!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            if (!result) return NotFound();

            return Ok(new { Message = "Role deleted successfully!" });
        }

    }
}
