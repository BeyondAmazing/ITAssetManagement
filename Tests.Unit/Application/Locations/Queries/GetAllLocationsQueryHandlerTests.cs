using Application.Features.Locations.Queries.GetAll;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Locations.Queries
{
    public class GetAllLocationsQueryHandlerTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly GetAllLocationsQueryHandler _handler;
        private readonly LocationFaker _locationFaker;

        public GetAllLocationsQueryHandlerTests()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _handler = new GetAllLocationsQueryHandler(_locationRepositoryMock.Object);
            _locationFaker = new LocationFaker();
            _locationFaker.UseSeed(123);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllLocations()
        {
            // Arrange
            var locations = _locationFaker.Generate(5);
            _locationRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(locations);

            var query = new GetAllLocationsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(locations);
            _locationRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoLocationsExist()
        {
            // Arrange
            var locations = new List<Location>();
            _locationRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(locations);

            var query = new GetAllLocationsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
            _locationRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}
