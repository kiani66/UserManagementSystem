using System.Threading.Tasks;
using UserManagementSystem.Application.DTOs.Requests;
using UserManagementSystem.Application.DTOs.Responses;

namespace UserManagementSystem.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse> GetProfileByUserIdAsync(int userId);
        Task CreateProfileAsync(CreateUserProfileRequest request);
        Task UpdateProfileAsync(UpdateUserProfileRequest request);
        Task DeleteProfileAsync(int userId);
    }
}
