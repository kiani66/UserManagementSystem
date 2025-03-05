using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using UserManagementSystem.Application.DTOs.Requests;

public class RoleControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RoleControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetRoles_ShouldReturnOk_WithListOfRoles()
    {
        var response = await _client.GetAsync("/api/roles");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateRole_ShouldReturnOk_WhenRoleIsCreated()
    {
        var createRoleRequest = new CreateRoleRequest
        {
            Name = "Manager",
            PermissionIds = new List<int> { 1, 2 }
        };

        var response = await _client.PostAsJsonAsync("/api/roles", createRoleRequest);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task AssignRole_ShouldReturnOk_WhenRoleIsAssigned()
    {
        var assignRoleRequest = new AssignRoleRequest
        {
            UserId = 1,
            RoleId = 2
        };

        var response = await _client.PostAsJsonAsync("/api/roles/assign", assignRoleRequest);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteRole_ShouldReturnOk_WhenRoleIsDeleted()
    {
        int roleId = 1;

        var response = await _client.DeleteAsync($"/api/roles/{roleId}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
