using UserManagementSystem.Domain.Entities;

namespace UserManagementSystem.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task UpdateAsync(User user);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
