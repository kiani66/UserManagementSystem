using Moq;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Interfaces;

public class UpdateRoleCommandHandlerTests
{
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly UpdateRoleCommandHandler _handler;

    public UpdateRoleCommandHandlerTests()
    {
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _handler = new UpdateRoleCommandHandler(_roleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_False_When_Role_Not_Found()
    {
        // Arrange
        var command = new UpdateRoleCommand(1, "UpdatedRole", new List<int> { 1, 2 });

        _roleRepositoryMock
            .Setup(repo => repo.GetRoleByIdAsync(command.RoleId))
            .ReturnsAsync((Role)null);  // شبیه‌سازی حالتی که نقش پیدا نمی‌شود

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Handle_Should_Update_Role_And_Return_True()
    {
        // Arrange
        var existingRole = new Role
        {
            Id = 1,
            Name = "OldRole",
            RolePermissions = new List<RolePermission> { new RolePermission { RoleId = 1, PermissionId = 1 } }
        };

        var command = new UpdateRoleCommand(1, "UpdatedRole", new List<int> { 2, 3 });

        _roleRepositoryMock
            .Setup(repo => repo.GetRoleByIdAsync(command.RoleId))
            .ReturnsAsync(existingRole);  // نقش یافت شد

        _roleRepositoryMock
            .Setup(repo => repo.UpdateRoleAsync(It.IsAny<Role>()))
            .ReturnsAsync(true); // شبیه‌سازی موفقیت در ذخیره تغییرات

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.Equal("UpdatedRole", existingRole.Name); // نام جدید بررسی شود
        Assert.Equal(2, existingRole.RolePermissions.Count); // دو مجوز اضافه شده است
    }

    [Fact]
    public async Task Handle_Should_Call_UpdateRoleAsync()
    {
        // Arrange
        var existingRole = new Role
        {
            Id = 1,
            Name = "OldRole",
            RolePermissions = new List<RolePermission>()
        };

        var command = new UpdateRoleCommand(1, "UpdatedRole", new List<int> { 2, 3 });

        _roleRepositoryMock
            .Setup(repo => repo.GetRoleByIdAsync(command.RoleId))
            .ReturnsAsync(existingRole);

        _roleRepositoryMock
            .Setup(repo => repo.UpdateRoleAsync(It.IsAny<Role>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _roleRepositoryMock.Verify(repo => repo.UpdateRoleAsync(It.IsAny<Role>()), Times.Once); // باید دقیقا یکبار صدا زده شود
    }
}
