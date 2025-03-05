namespace UserManagementSystem.Application.DTOs.Requests
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public List<int> PermissionIds { get; set; } // مجوزهایی که به این نقش تعلق دارند
    }
}
