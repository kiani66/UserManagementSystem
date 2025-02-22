using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Data;
using UserManagementSystem.Infrastructure.Interfaces;

namespace UserManagementSystem.Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetByUserIdAsync(int userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);
        }

        public async Task CreateAsync(UserProfile userProfile)
        {
            await _context.UserProfiles.AddAsync(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
        }
    }
}
