using Application.Features.StatusLabels.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Tests.Common.Fakers;

namespace Tests.Integration.Controllers;

public class StatusLabelsControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;
    private readonly StatusLabelFaker _statusLabelFaker;

    public StatusLabelsControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _statusLabelFaker = new StatusLabelFaker();
        _statusLabelFaker.UseSeed(123);
    }

    [Fact]
    public async Task GetAllStatusLabels_EmptyDatabase_ReturnsEmptyList()
    {
        // Act
        var response = await _fixture.Client.GetAsync("/api/v1/statuslabels");

        // Assert
        response.EnsureSuccessStatusCode();
        var statusLabels = await response.Content.ReadFromJsonAsync<List<StatusLabel>>();
        statusLabels.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateStatusLabel_ValidRequest_ReturnsCreatedStatusLabel()
    {
        // Arrange
        var statusLabel = _statusLabelFaker.Generate();
        var command = new CreateStatusLabelCommand(statusLabel.Name, statusLabel.Deployable, statusLabel.Pending, statusLabel.Archived);

        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/statuslabels", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdStatusLabel = await response.Content.ReadFromJsonAsync<StatusLabel>();
        createdStatusLabel.Should().NotBeNull();
        createdStatusLabel!.Name.Should().Be(statusLabel.Name);

        using var scope = _fixture.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DbCtx>();
        var dbStatusLabel = await dbContext.StatusLabels.FirstOrDefaultAsync(sl => sl.Name == statusLabel.Name);
        dbStatusLabel.Should().NotBeNull();
        dbStatusLabel.Name.Should().Be(statusLabel.Name);
    }

    [Fact]
    public async Task CreateStatusLabel_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var command = new CreateStatusLabelCommand("", true, false, false);

        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/statuslabels", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
