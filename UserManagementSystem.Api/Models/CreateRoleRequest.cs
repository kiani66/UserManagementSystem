namespace UserManagementSystem.Api.Models
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
