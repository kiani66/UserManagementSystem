using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Application.Interfaces;

namespace UserManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/userprofiles")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var profile = await _userProfileService.GetProfileByUserIdAsync(userId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] CreateUserProfileRequest request)
        {
            await _userProfileService.CreateProfileAsync(request);
            return Ok(new { Message = "User Profile Created Successfully" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequest request)
        {
            await _userProfileService.UpdateProfileAsync(request);
            return Ok(new { Message = "User Profile Updated Successfully" });
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteProfile(int userId)
        {
            await _userProfileService.DeleteProfileAsync(userId);
            return Ok(new { Message = "User Profile Deleted Successfully" });
        }
    }
}
