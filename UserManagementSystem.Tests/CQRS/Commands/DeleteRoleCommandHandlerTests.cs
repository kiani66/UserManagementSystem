using FluentAssertions;
using Moq;
using UserManagementSystem.Application.CQRS.Commands.Role;
using UserManagementSystem.Infrastructure.Interfaces;

public class DeleteRoleCommandHandlerTests
{
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly DeleteRoleCommandHandler _handler;

    public DeleteRoleCommandHandlerTests()
    {
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _handler = new DeleteRoleCommandHandler(_roleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteRole_WhenRoleExists()
    {
        // Arrange
        var command = new DeleteRoleCommand(1);

        _roleRepositoryMock.Setup(repo => repo.DeleteRoleAsync(1))
                           .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _roleRepositoryMock.Verify(repo => repo.DeleteRoleAsync(1), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenRoleDoesNotExist()
    {
        // Arrange
        var command = new DeleteRoleCommand(999);

        _roleRepositoryMock.Setup(repo => repo.DeleteRoleAsync(999))
                           .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }
}
