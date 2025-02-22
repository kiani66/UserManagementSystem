using UserManagementSystem.Domain.Entities;

namespace UserManagementSystem.Infrastructure.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetByUserIdAsync(int userId);
        Task CreateAsync(UserProfile userProfile);
        Task UpdateAsync(UserProfile userProfile);
        Task DeleteAsync(UserProfile userProfile);
    }
}
