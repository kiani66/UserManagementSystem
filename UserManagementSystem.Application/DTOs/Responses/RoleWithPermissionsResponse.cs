
namespace UserManagementSystem.Application.DTOs.Responses
{
    public class RoleWithPermissionsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PermissionResponse> Permissions { get; set; }
    }
}
