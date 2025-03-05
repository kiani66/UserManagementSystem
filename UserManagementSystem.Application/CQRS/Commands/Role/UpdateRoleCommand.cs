using MediatR;

public class UpdateRoleCommand : IRequest<bool>
{
    public int RoleId { get; set; }
    public string Name { get; set; }
    public List<int> Permissions { get; set; }

    public UpdateRoleCommand(int roleId, string name, List<int> permissions)
    {
        RoleId = roleId;
        Name = name;
        Permissions = permissions;
    }
}
