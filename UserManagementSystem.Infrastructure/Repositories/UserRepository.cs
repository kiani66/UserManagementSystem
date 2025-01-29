using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Data;
using UserManagementSystem.Infrastructure.Interfaces;

namespace UserManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync() => await _context.Users.ToListAsync();

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _context.Users.AsNoTracking().FirstOrDefaultAsync(c=>c.Email == email);
        }
    }
}
