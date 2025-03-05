using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using UserManagementSystem.Application.DTOs.Requests;

public class UserControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUsers_ShouldReturnOk_WithListOfUsers()
    {
        // Act
        var response = await _client.GetAsync("/api/users");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateUser_ShouldReturnOk_WhenUserIsCreated()
    {
        // Arrange
        var createUserRequest = new CreateUserRequest
        {
            Email = "newuser@example.com",
            Password = "NewUser123!",
            Name = "New User"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/users", createUserRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnOk_WhenUserIsDeleted()
    {
        // Arrange
        int userId = 1;

        // Act
        var response = await _client.DeleteAsync($"/api/users/{userId}");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
