using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Application.DTOs.Requests;
using UserManagementSystem.Application.DTOs.Responses;

namespace UserManagementSystem.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly Application.Interfcaces.IUserService _userService;

        public UserController(Application.Interfcaces.IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<UserResponse>>
            {
                Success = true,
                Message = "User list retrieved successfully.",
                Data = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            await _userService.CreateAsync(user);
            return Ok(new { Message = "User created successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteByIdAsync(id);
            if (!deleted)
                return NotFound(new ApiResponse<string> { Success = false, Message = "User not found." });

            return Ok(new ApiResponse<string> { Success = true, Message = "User deleted successfully." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            if (request == null)
                return BadRequest(new ApiResponse<string> { Success = false, Message = "Invalid data." });

            var updated = await _userService.UpdateAsync(id, request);
            if (!updated)
                return NotFound(new ApiResponse<string> { Success = false, Message = "User not found." });

            return Ok(new ApiResponse<string> { Success = true, Message = "User updated successfully." });
        }


    }

}

