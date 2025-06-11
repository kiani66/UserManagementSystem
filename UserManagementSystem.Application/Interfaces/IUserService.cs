

using UserManagementSystem.Application.DTOs.Requests;
using UserManagementSystem.Application.DTOs.Responses;

namespace UserManagementSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse?> GetByEmailAsync(string email);
        Task<UserWithPassword?> GetWithPasswordByEmailAsync(string email);
        Task<IEnumerable<UserResponse>> GetAllAsync();
        Task<UserResponse> GetByIdAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<int> CreateAsync(CreateUserRequest request);
        Task<bool> UpdateAsync(int id, UpdateUserRequest request);
        Task<bool> DeleteByIdAsync(int id);
    }
}
