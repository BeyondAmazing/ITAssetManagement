using Application.Features.Users.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Tests.Common.Fakers;

namespace Tests.Integration.Controllers;

public class UsersControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;
    private readonly UserFaker _userFaker;

    public UsersControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _userFaker = new UserFaker();
        _userFaker.UseSeed(123);
    }

    [Fact]
    public async Task GetAllUsers_EmptyDatabase_ReturnsEmptyList()
    {
        // Act - Send Get request to fetch all users
        var response = await _fixture.Client.GetAsync("/api/v1/users");

        // Assert - Verify response is successful and list is empty
        response.EnsureSuccessStatusCode();
        var users = await response.Content.ReadFromJsonAsync<List<User>>();
        users.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateUser_ValidRequest_ReturnsCreatedUser()
    {
        var user = _userFaker.Generate();
        var command = new CreateUserCommand(user.Username, user.FirstName, user.LastName, user.Email, user.CompanyId);

        // Act - Send POST request to create the user
        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/users", command);

        // Assert - Verify response and database state
        response.EnsureSuccessStatusCode();
        var createdUser = await response.Content.ReadFromJsonAsync<User>();
        createdUser.Should().NotBeNull();
        createdUser!.Username.Should().Be(user.Username);

        // Check the database to ensure the user was saved
        using var scope = _fixture.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DbCtx>();
        var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        dbUser.Should().NotBeNull();
        dbUser!.FirstName.Should().Be(user.FirstName);
    }

    [Fact]
    public async Task CreateUser_InvalidRequest_ReturnsBadRequest()
    {
        var command = new CreateUserCommand("", "", "", "invalid-email", Guid.Empty);

        // Act - Send POST request to create the user
        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/users", command);

        // Assert - Verify response is bad request
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
