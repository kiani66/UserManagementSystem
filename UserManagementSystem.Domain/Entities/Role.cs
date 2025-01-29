
namespace UserManagementSystem.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } // مانند Admin, User, Manager
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    }
}
