using Application.Features.StatusLabels.Queries.GetAll;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.StatusLabels.Queries
{
    public class GetAllStatusLabelsQueryHandlerTests
    {
        private readonly Mock<IStatusLabelRepository> _statusLabelRepositoryMock;
        private readonly GetAllStatusLabelsQueryHandler _handler;

        public GetAllStatusLabelsQueryHandlerTests()
        {
            _statusLabelRepositoryMock = new Mock<IStatusLabelRepository>();
            _handler = new GetAllStatusLabelsQueryHandler(_statusLabelRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllStatusLabels()
        {
            // Arrange
            var statusLabels = new StatusLabelFaker().UseSeed(123).Generate(5);
            _statusLabelRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(statusLabels);

            // Act
            var result = await _handler.Handle(new GetAllStatusLabelsQuery(), default);

            // Assert
            Assert.Equal(statusLabels.Count, result.Count());
            Assert.Equal(statusLabels, result);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoStatusLabelsExist()
        {
            // Arrange
            _statusLabelRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<StatusLabel>());

            // Act
            var result = await _handler.Handle(new GetAllStatusLabelsQuery(), default);

            // Assert
            Assert.Empty(result);
        }
    }
}
