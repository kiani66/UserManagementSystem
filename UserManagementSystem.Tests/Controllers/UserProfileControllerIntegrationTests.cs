using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

public class UserProfileControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserProfileControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProfile_ShouldReturnOk_WhenProfileExists()
    {
        // Arrange
        int userId = 1;

        // Act
        var response = await _client.GetAsync($"/api/userprofiles/{userId}");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().Contain("FullName");
    }

    [Fact]
    public async Task GetProfile_ShouldReturnNotFound_WhenProfileDoesNotExist()
    {
        // Arrange
        int userId = 999;

        // Act
        var response = await _client.GetAsync($"/api/userprofiles/{userId}");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateProfile_ShouldReturnOk_WhenProfileIsCreated()
    {
        // Arrange
        var createProfileRequest = new CreateUserProfileRequest
        {
            UserId = 2,
            FullName = "John Doe",
            Address = "123 Main St",
            PhoneNumber = "1234567890"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/userprofiles", createProfileRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().Contain("User profile created successfully");
    }

    [Fact]
    public async Task CreateProfile_ShouldReturnBadRequest_WhenRequestIsInvalid()
    {
        // Arrange
        var createProfileRequest = new CreateUserProfileRequest
        {
            UserId = 0, // Invalid UserId
            FullName = "",
            Address = "123 Main St",
            PhoneNumber = "1234567890"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/userprofiles", createProfileRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateProfile_ShouldReturnOk_WhenProfileIsUpdated()
    {
        // Arrange
        int userId = 1;
        var updateProfileRequest = new UpdateUserProfileRequest
        {
            FullName = "Jane Doe",
            Address = "456 Main St",
            PhoneNumber = "0987654321"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/userprofiles/{userId}", updateProfileRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().Contain("User profile updated successfully");
    }

    [Fact]
    public async Task UpdateProfile_ShouldReturnNotFound_WhenProfileDoesNotExist()
    {
        // Arrange
        int userId = 999;
        var updateProfileRequest = new UpdateUserProfileRequest
        {
            FullName = "Jane Doe",
            Address = "456 Main St",
            PhoneNumber = "0987654321"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/userprofiles/{userId}", updateProfileRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteProfile_ShouldReturnOk_WhenProfileIsDeleted()
    {
        // Arrange
        int userId = 1;

        // Act
        var response = await _client.DeleteAsync($"/api/userprofiles/{userId}");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().Contain("User profile deleted successfully");
    }

    [Fact]
    public async Task DeleteProfile_ShouldReturnNotFound_WhenProfileDoesNotExist()
    {
        // Arrange
        int userId = 999;

        // Act
        var response = await _client.DeleteAsync($"/api/userprofiles/{userId}");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
