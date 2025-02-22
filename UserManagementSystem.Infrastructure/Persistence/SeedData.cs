using System.Linq;
using System.Collections.Generic;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Infrastructure.Data;

namespace UserManagementSystem.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // مقداردهی `Permissions`
            if (!context.Permissions.Any())
            {
                var initialPermissions = new List<Permission>
                {
                    new Permission { Name = "ViewUsers" },
                    new Permission { Name = "EditUsers" },
                    new Permission { Name = "DeleteUsers" },
                    new Permission { Name = "ViewRoles" },
                    new Permission { Name = "ManageRoles" }
                };
                context.Permissions.AddRange(initialPermissions);
                context.SaveChanges();
            }

            // مقداردهی `Roles`
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin" },
                    new Role { Name = "User" },
                    new Role { Name = "Manager" }
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            // دریافت `RoleId` ها از دیتابیس
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            var userRole = context.Roles.FirstOrDefault(r => r.Name == "User");
            var managerRole = context.Roles.FirstOrDefault(r => r.Name == "Manager");

            // دریافت `PermissionId` ها از دیتابیس
            var permissions = context.Permissions.ToDictionary(p => p.Name, p => p.Id);

            if (!context.RolePermissions.Any() && adminRole != null && permissions.Count > 0)
            {
                var rolePermissions = new List<RolePermission>
                {
                    new RolePermission { RoleId = adminRole.Id, PermissionId = permissions["ViewUsers"] },
                    new RolePermission { RoleId = adminRole.Id, PermissionId = permissions["EditUsers"] },
                    new RolePermission { RoleId = adminRole.Id, PermissionId = permissions["DeleteUsers"] },
                    new RolePermission { RoleId = adminRole.Id, PermissionId = permissions["ViewRoles"] },
                    new RolePermission { RoleId = adminRole.Id, PermissionId = permissions["ManageRoles"] },

                    new RolePermission { RoleId = userRole.Id, PermissionId = permissions["ViewUsers"] },

                    new RolePermission { RoleId = managerRole.Id, PermissionId = permissions["ViewUsers"] },
                    new RolePermission { RoleId = managerRole.Id, PermissionId = permissions["EditUsers"] }
                };

                context.RolePermissions.AddRange(rolePermissions);
                context.SaveChanges();
            }

            // مقداردهی `Users`
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { Name = "admin", Email = "admin@example.com", PasswordHash = HashPassword("Admin123!"), RoleId = adminRole.Id, CreatedAt = DateTime.UtcNow },
                    new User { Name = "user", Email = "user@example.com", PasswordHash = HashPassword("User123!"), RoleId = userRole.Id, CreatedAt = DateTime.UtcNow },
                    new User { Name = "manager", Email = "manager@example.com", PasswordHash = HashPassword("Manager123!"), RoleId = managerRole.Id, CreatedAt = DateTime.UtcNow }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            // دریافت `UserId` ها از دیتابیس
            var adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@example.com");
            var normalUser = context.Users.FirstOrDefault(u => u.Email == "user@example.com");
            var managerUser = context.Users.FirstOrDefault(u => u.Email == "manager@example.com");

            // مقداردهی `UserRoles`
            if (!context.UserRoles.Any() && adminUser != null && adminRole != null)
            {
                var userRoles = new List<UserRole>
                {
                    new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id },
                    new UserRole { UserId = normalUser.Id, RoleId = userRole.Id },
                    new UserRole { UserId = managerUser.Id, RoleId = managerRole.Id }
                };

                context.UserRoles.AddRange(userRoles);
                context.SaveChanges();
            }
        }

        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
