using Moq;
using UserManagementSystem.Application.CQRS.Commands.Role;
using UserManagementSystem.Application.CQRS.Handlers.Roles;
using UserManagementSystem.Infrastructure.Interfaces;

public class AssignRoleToUserCommandHandlerTests
{
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly AssignRoleToUserCommandHandler _handler;

    public AssignRoleToUserCommandHandlerTests()
    {
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _handler = new AssignRoleToUserCommandHandler(_roleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Assign_Role_To_User()
    {
        // Arrange
        var command = new AssignRoleToUserCommand(1, 2);

        _roleRepositoryMock.Setup(repo => repo.AssignRoleToUserAsync(command.UserId, command.RoleId))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
    }
}
