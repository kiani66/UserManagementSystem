using Microsoft.AspNetCore.Mvc;
using System.Text;
using UserManagementSystem.Api.Models;
using UserManagementSystem.Application.DTOs.Responses;
using UserManagementSystem.Application.Interfcaces;

namespace UserManagementSystem.Api.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Application.DTOs.Requests.CreateUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Name))
                return BadRequest(new ApiResponse<string> { Success = false, Message = "All fields are required." });

            var existingUsers = await _userService.GetAllAsync();
            if (existingUsers.Any(u => u.Email == request.Email))
                return BadRequest(new ApiResponse<string> { Success = false, Message = "User with this email already exists." });

            var userId = await _userService.CreateAsync(request);

            return Ok(new ApiResponse<int> { Success = true, Message = "User registered successfully!", Data = userId });

        }
    }
}
