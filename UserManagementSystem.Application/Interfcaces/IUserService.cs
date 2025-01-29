using UserManagementSystem.Domain.Entities;

namespace UserManagementSystem.Application.Interfcaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
