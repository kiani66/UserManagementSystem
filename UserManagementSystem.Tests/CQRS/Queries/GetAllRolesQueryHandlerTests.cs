using Moq;
using UserManagementSystem.Application.CQRS.Queries.Role;
using UserManagementSystem.Application.CQRS.Handlers.Roles;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Interfaces;

public class GetAllRolesQueryHandlerTests
{
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly GetAllRolesQueryHandler _handler;

    public GetAllRolesQueryHandlerTests()
    {
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _handler = new GetAllRolesQueryHandler(_roleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnRoles_WhenRolesExist()
    {
        // Arrange
        var roles = new List<Role>
        {
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" }
        };

        _roleRepositoryMock.Setup(repo => repo.GetAllRolesAsync())
                           .ReturnsAsync(roles);

        var query = new GetAllRolesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, r => r.Name == "Admin");
        Assert.Contains(result, r => r.Name == "User");
    }
}
