using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Application.AssetModels.Queries;
using Domain.Entities;
using Infrastructure.Fakers;

namespace Tests.Unit.Application.AssetModels.Queries
{
    public class GetAllAssetModelsQueryHandlerTests
    {
        private readonly GetAllAssetModelsQueryHandler _handler;
        private readonly AssetModelFaker _assetModelFaker;
        private readonly Mock<IAssetModelRepository> _assetModelRepositoryMock;

        public GetAllAssetModelsQueryHandlerTests()
        {
            _assetModelRepositoryMock = new Mock<IAssetModelRepository>();
            _handler = new GetAllAssetModelsQueryHandler(_assetModelRepositoryMock.Object);
            _assetModelFaker = new AssetModelFaker();
            _assetModelFaker.UseSeed(123);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllAssetModels()
        {
            // Arrange
            var expectedAssetModels = _assetModelFaker.Generate(5);
            _assetModelRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                                     .ReturnsAsync(expectedAssetModels);

            // Act
            var result = await _handler.Handle(new GetAllAssetModelsQuery(), default);

            // Assert
            Assert.Equal(expectedAssetModels.Count, result.Count);
            Assert.Equal(expectedAssetModels, result);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoAssetModelsExist()
        {
            // Arrange
            var expectedAssetModels = new List<AssetModel>();
            _assetModelRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                                     .ReturnsAsync(expectedAssetModels);

            // Act
            var result = await _handler.Handle(new GetAllAssetModelsQuery(), default);

            // Assert
            Assert.Empty(result);
        }
    }
}
