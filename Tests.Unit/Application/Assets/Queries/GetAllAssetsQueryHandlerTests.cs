using Application.Features.Assets.Queries.GetAll;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Assets.Queries
{
    public class GetAllAssetsQueryHandlerTests
    {
        private readonly Mock<IAssetRepository> _assetRepositoryMock;
        private readonly GetAllAssetsQueryHandler _handler;
        private readonly AssetFaker _assetFaker;
        public GetAllAssetsQueryHandlerTests()
        {
            _assetRepositoryMock = new Mock<IAssetRepository>();
            _handler = new GetAllAssetsQueryHandler(_assetRepositoryMock.Object);
            _assetFaker = new AssetFaker();
            _assetFaker.UseSeed(123);
        }

        [Fact]
        public async Task Handle_ReturnsAllAssets()
        {
            // Arrange
            var assets = _assetFaker.Generate(2);
            _assetRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(assets);

            // Act
            var result = await _handler.Handle(new GetAllAssetsQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }
    }
}
