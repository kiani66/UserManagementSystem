using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using UserManagementSystem.Application.DTOs.Requests;

public class RegisterControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RegisterControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenUserIsCreated()
    {
        var registerRequest = new CreateUserRequest
        {
            Email = "newuser@example.com",
            Password = "NewUser123!",
            Name = "New User",
            RoleId = 2
        };

        var response = await _client.PostAsJsonAsync("/api/register", registerRequest);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
