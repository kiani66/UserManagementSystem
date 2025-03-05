using UserManagementSystem.Domain.Entities;

namespace UserManagementSystem.Infrastructure.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetProfileByUserIdAsync(int userId);
        Task CreateProfileAsync(UserProfile userProfile);
        Task UpdateProfileAsync(UserProfile userProfile);
        Task DeleteProfileAsync(UserProfile userProfile);
    }
}
