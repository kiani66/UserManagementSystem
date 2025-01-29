using System.Linq;
using System.Collections.Generic;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Data;

namespace UserManagementSystem.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // بررسی و مقداردهی Permissions
            if (!context.Permissions.Any())
            {
                var permissions = new List<Permission>
                {
                    new Permission { Name = "ViewUsers" },
                    new Permission { Name = "EditUsers" },
                    new Permission { Name = "DeleteUsers" },
                    new Permission { Name = "ViewRoles" },
                    new Permission { Name = "ManageRoles" }
                };
                context.Permissions.AddRange(permissions);
                context.SaveChanges();
            }

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
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            var userRole = context.Roles.FirstOrDefault(r => r.Name == "User");
            var managerRole = context.Roles.FirstOrDefault(r => r.Name == "Manager");

            var viewUsers = context.Permissions.FirstOrDefault(p => p.Name == "ViewUsers");
            var editUsers = context.Permissions.FirstOrDefault(p => p.Name == "EditUsers");
            var deleteUsers = context.Permissions.FirstOrDefault(p => p.Name == "DeleteUsers");
            var viewRoles = context.Permissions.FirstOrDefault(p => p.Name == "ViewRoles");
            var manageRoles = context.Permissions.FirstOrDefault(p => p.Name == "ManageRoles");

            if (!context.RolePermissions.Any() && adminRole != null && viewUsers != null)
            {
                var rolePermissions = new List<RolePermission>
                {
                    new RolePermission { RoleId = adminRole.Id, PermissionId = viewUsers.Id },
                    new RolePermission { RoleId = adminRole.Id, PermissionId = editUsers.Id },
                    new RolePermission { RoleId = adminRole.Id, PermissionId = deleteUsers.Id },
                    new RolePermission { RoleId = adminRole.Id, PermissionId = viewRoles.Id },
                    new RolePermission { RoleId = adminRole.Id, PermissionId = manageRoles.Id },

                    new RolePermission { RoleId = userRole.Id, PermissionId = viewUsers.Id },

                    new RolePermission { RoleId = managerRole.Id, PermissionId = viewUsers.Id },
                    new RolePermission { RoleId = managerRole.Id, PermissionId = editUsers.Id }
                };

                context.RolePermissions.AddRange(rolePermissions);
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { Name="admin", Email = "admin@example.com", PasswordHash = HashPassword("Admin123!"), Role = "Admin" },
                    new User { Name="user", Email = "user@example.com", PasswordHash = HashPassword("User123!"), Role = "User" },
                    new User { Name="manager", Email = "manager@example.com", PasswordHash = HashPassword("Manager123!"), Role = "Manager" }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            // دریافت `Id` ها بعد از مقداردهی
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
