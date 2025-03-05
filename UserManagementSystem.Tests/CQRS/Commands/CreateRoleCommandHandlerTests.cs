using Microsoft.EntityFrameworkCore;
using Moq;
using UserManagementSystem.Application.CQRS.Commands.Role;
using UserManagementSystem.Application.CQRS.Handlers.Roles;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Interfaces;

public class CreateRoleCommandHandlerTests
{
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly CreateRoleCommandHandler _handler;

    public CreateRoleCommandHandlerTests()
    {
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _handler = new CreateRoleCommandHandler(_roleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateRole_WhenValidRequest()
    {
        // Arrange
        var command = new CreateRoleCommand("Admin", new List<int> { 1, 2, 3 });
        _roleRepositoryMock.Setup(repo => repo.AddRoleAsync(It.IsAny<Role>()))
                           .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var createdRole = await _roleRepositoryMock.Object.GetRoleByIdAsync(result);
        Assert.NotNull(createdRole);
        Assert.Equal("Admin", createdRole.Name);
        _roleRepositoryMock.Verify(repo => repo.AddRoleAsync(It.IsAny<Role>()), Times.Once);
    }
}
