using Microsoft.AspNetCore.Mvc;
using System.Text;
using UserManagementSystem.Api.Models;
using UserManagementSystem.Application.Interfcaces;
using UserManagementSystem.Domain.Entities;

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
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Name))
                return BadRequest("All fields are required.");

            var existingUsers = await _userService.GetAllUsersAsync();
            if (existingUsers.Any(u => u.Email == request.Email))
                return BadRequest("User with this email already exists.");

            string hashedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));

            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = hashedPassword,
                Role = request.Role
            };

            await _userService.CreateUserAsync(newUser);

            return Ok(new { Message = "User registered successfully!" });
        }
    }
}
