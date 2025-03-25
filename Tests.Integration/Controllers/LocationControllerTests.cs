using Application.Features.Locations.Commands.Create;
using Domain.Entities;
using System.Net;
using System.Net.Http.Json;

namespace Tests.Integration.Controllers;

public class LocationControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;

    public LocationControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateLocation_ValidRequest_ReturnsCreatedLocation()
    {
        string Name = "Test Location", Address = "Test Address", City = "Test City", State = "Test State", Country = "Test Country";
        // Arrange
        var command = new CreateLocationCommand(Name, Address, City, State, Country, null);
        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/locations", command);
        // Assert
        response.EnsureSuccessStatusCode();
        var locationResponse = await response.Content.ReadFromJsonAsync<Location>();
        Assert.NotNull(locationResponse);
        Assert.Equal(command.name, locationResponse.Name);
        Assert.Equal(command.address, locationResponse.Address);
        Assert.Equal(command.city, locationResponse.City);
        Assert.Equal(command.state, locationResponse.State);
        Assert.Equal(command.country, locationResponse.Country);
    }

    [Fact]
    public async Task CreateLocation_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var command = new CreateLocationCommand(null, null, null, null, null, null);
        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/locations", command);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetAllLocations_ReturnsListOfLocations()
    {
        // Act
        var response = await _fixture.Client.GetAsync("/api/v1/locations");
        // Assert
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<IEnumerable<Location>>();
        Assert.NotNull(locations);
        Assert.NotEmpty(locations);
    }

    [Fact]
    public async Task GetAllLocations_NoLocations_ReturnsEmptyList()
    {
        // Arrange
        // Ensure the database is empty or mock the response
        // Act
        var response = await _fixture.Client.GetAsync("/api/v1/locations");
        // Assert
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<IEnumerable<Location>>();
        Assert.NotNull(locations);
        Assert.Empty(locations);
    }

    [Fact]
    public async Task GetLocationById_ValidId_ReturnsLocation()
    {
        // Arrange
        var locationId = Guid.NewGuid(); // Use an existing location ID or create a new one for testing
        // Act
        var response = await _fixture.Client.GetAsync($"/api/v1/locations/{locationId}");
        // Assert
        response.EnsureSuccessStatusCode();
        var location = await response.Content.ReadFromJsonAsync<Location>();
        Assert.NotNull(location);
        Assert.Equal(locationId, location.Id);
    }
}
