using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentAssertions;
using UserManagementSystem.Application.Interfaces;
using UserManagementSystem.Application.Services;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Interfaces;
using AutoMapper;

public class UserProfileServiceTests
{
    private readonly Mock<IUserProfileRepository> _mockUserProfileRepository;
    private readonly IUserProfileService _userProfileService;
    private readonly Mock<IMapper> _mockMapper;


    public UserProfileServiceTests()
    {
        _mockUserProfileRepository = new Mock<IUserProfileRepository>();
        _mockMapper = new Mock<IMapper>();
        _userProfileService = new UserProfileService(_mockUserProfileRepository.Object, _mockMapper.Object);

    }

    [Fact]
    public async Task GetProfileByUserIdAsync_ShouldReturnProfile_WhenProfileExists()
    {
        // Arrange
        int userId = 1;
        var expectedProfile = new UserProfile
        {
            UserId = userId,
            Address = "123 Main St",
            PhoneNumber = "1234567890"
        };

        _mockUserProfileRepository.Setup(repo => repo.GetProfileByUserIdAsync(userId))
                                  .ReturnsAsync(expectedProfile);

        // Act
        var result = await _userProfileService.GetProfileByUserIdAsync(userId);

        // Assert
        result.Should().NotBeNull();
        result.FullName.Should().Be("John Doe");
    }

    [Fact]
    public async Task GetProfileByUserIdAsync_ShouldReturnNull_WhenProfileDoesNotExist()
    {
        // Arrange
        int userId = 999;
        _mockUserProfileRepository.Setup(repo => repo.GetProfileByUserIdAsync(userId))
                                  .ReturnsAsync((UserProfile)null);

        // Act
        var result = await _userProfileService.GetProfileByUserIdAsync(userId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateProfileAsync_ShouldComplete_WhenProfileIsCreated()
    {
        // Arrange
        var newProfileRequest = new CreateUserProfileRequest
        {
            UserId = 2,
            FullName = "Jane Doe",
            Address = "456 Another St",
            PhoneNumber = "0987654321"
        };

        _mockUserProfileRepository.Setup(repo => repo.CreateProfileAsync(It.IsAny<UserProfile>()))
                                  .Returns(Task.CompletedTask);

        // Act
        await _userProfileService.CreateProfileAsync(newProfileRequest);

        // Assert
        _mockUserProfileRepository.Verify(repo => repo.CreateProfileAsync(It.IsAny<UserProfile>()), Times.Once);
    }
}


