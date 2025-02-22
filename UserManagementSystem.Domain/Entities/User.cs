namespace UserManagementSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; } // نام ممکن است مقدار نداشته باشد
        public string Email { get; set; }
        public string PasswordHash { get; set; } // هش‌شده ذخیره شود
        public int RoleId { get; set; } // مرتبط با جدول `Roles`
        public Role Role { get; set; } // Navigation Property
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }


        public UserProfile UserProfile { get; set; }
    }
}
