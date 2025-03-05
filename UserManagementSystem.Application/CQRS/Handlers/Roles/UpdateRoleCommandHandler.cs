using MediatR;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Interfaces;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
{
    private readonly IRoleRepository _roleRepository;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        // دریافت نقش از دیتابیس
        var existingRole = await _roleRepository.GetRoleByIdAsync(request.RoleId);
        if (existingRole == null)
            return false;

        // به‌روزرسانی نام نقش
        existingRole.Name = request.Name;

        // این قسمت بستگی به پیاده‌سازی `Permissions` دارد
        existingRole.RolePermissions.Clear();
        foreach (var permissionId in request.Permissions)
        {
            existingRole.RolePermissions.Add(new RolePermission
            {
                RoleId = existingRole.Id,
                PermissionId = permissionId
            });
        }

        // ذخیره تغییرات
        return await _roleRepository.UpdateRoleAsync(existingRole);
    }
}
