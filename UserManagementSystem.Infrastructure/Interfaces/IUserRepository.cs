using UserManagementSystem.Domain.Entities;

namespace UserManagementSystem.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task AddUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
