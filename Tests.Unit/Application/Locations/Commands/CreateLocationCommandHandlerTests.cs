using Application.Features.Locations.Commands.Create;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Locations.Commands;

public class CreateLocationCommandHandlerTests
{
    private readonly Mock<ILocationRepository> _locationRepositoryMock;
    private readonly CreateLocationCommandHandler _handler;
    private readonly LocationFaker _locationFaker;

    public CreateLocationCommandHandlerTests()
    {
        _locationRepositoryMock = new Mock<ILocationRepository>();
        _handler = new CreateLocationCommandHandler(_locationRepositoryMock.Object);
        _locationFaker = new LocationFaker();
        _locationFaker.UseSeed(123);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreateLocation()
    {
        // Arrange
        var location = _locationFaker.Generate();
        var command = new CreateLocationCommand(location.Name, location.Address, location.City, location.State, location.Country, location.ParentId);
        var expectedLocation = Location.Create(location.Name, location.Address, location.City, location.State, location.Country, location.ParentId);
        _locationRepositoryMock.Setup(r => r.AddAsync(It.Is<Location>(l => l.Name == command.name))).ReturnsAsync(expectedLocation);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(location.Name);
        result.ParentId.Should().Be(location.ParentId);
        _locationRepositoryMock.Verify(r => r.AddAsync(It.Is<Location>(l => l.Name == location.Name)), Times.Once);
    }
}
