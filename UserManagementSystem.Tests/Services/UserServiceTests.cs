using Moq;
using FluentAssertions;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Application.DTOs.Requests;
using UserManagementSystem.Application.Services;
using UserManagementSystem.Infrastructure.Interfaces;
using UserManagementSystem.Application.Interfaces;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnListOfUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };

        _mockUserRepository.Setup(x => x.GetAllAsync())
                           .ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);

    }

    [Fact]
    public async Task CreateUserAsync_ShouldCallRepository_WhenUserIsValid()
    {
        var request = new CreateUserRequest
        {
            Name = "New User",
            Email = "newuser@example.com",
            Password = "Password123",
            RoleId = 1
        };

        _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>()))
                           .Returns(Task.CompletedTask);

        await _userService.CreateAsync(request);

        _mockUserRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task GetUserByEmailAsync_ShouldReturnUser_WhenUserExists()
    {
        var email = "test@example.com";
        var existingUser = new User
        {
            Id = 1,
            Name = "Existing User",
            Email = email
        };

        _mockUserRepository.Setup(x => x.GetByEmailAsync(email))
                           .ReturnsAsync(existingUser);

        var result = await _userService.GetByEmailAsync(email);

        result.Should().NotBeNull();
        result.Email.Should().Be(email);
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldCallRepository_WhenUserExists()
    {
        int userId = 1;

        _mockUserRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
                   .ReturnsAsync(true); 

        await _userService.DeleteByIdAsync(userId);

        _mockUserRepository.Verify(x => x.DeleteByIdAsync(It.IsAny<int>()), Times.Once);
    }
}
