using Moq;
using FluentAssertions;
using UserManagementSystem.Application.CQRS.Queries.Role;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Interfaces;

public class GetRoleByIdQueryHandlerTests
{
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly GetRoleByIdQueryHandler _handler;

    public GetRoleByIdQueryHandlerTests()
    {
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _handler = new GetRoleByIdQueryHandler(_roleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnRole_WhenRoleExists()
    {
        // Arrange
        var role = new Role { Id = 1, Name = "Admin" };
        _roleRepositoryMock.Setup(repo => repo.GetRoleByIdAsync(1))
                           .ReturnsAsync(role);

        var query = new GetRoleByIdQuery(1);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Admin");
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenRoleDoesNotExist()
    {
        // Arrange
        _roleRepositoryMock.Setup(repo => repo.GetRoleByIdAsync(999))
                           .ReturnsAsync((Role?)null);

        var query = new GetRoleByIdQuery(999);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
