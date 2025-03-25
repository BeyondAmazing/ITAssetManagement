using Application.Features.Assets.Commands.Create;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Assets.Commands;

public class CreateAssetCommandHandlerTests
{
    private readonly Mock<IAssetRepository> _assetRepositoryMock;
    private readonly CreateAssetCommandHandler _handler;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AssetFaker _assetFaker;

    public CreateAssetCommandHandlerTests()
    {
        _assetRepositoryMock = new Mock<IAssetRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateAssetCommandHandler(_assetRepositoryMock.Object, _mapperMock.Object);
        _assetFaker = new AssetFaker();
        _assetFaker.UseSeed(123);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesAsset()
    {
        var fakeAsset = _assetFaker.Generate();
        // Arrange
        var command = new CreateAssetCommand(
            fakeAsset.Name,
            fakeAsset.AssetTag,
            fakeAsset.Serial,
            fakeAsset.ModelId,
            fakeAsset.StatusId,
            fakeAsset.CompanyId,
            fakeAsset.UserId,
            fakeAsset.LocationId,
            fakeAsset.SupplierId,
            fakeAsset.DepreciationId,
            fakeAsset.PurchaseDate,
            fakeAsset.PurchaseCost,
            fakeAsset.Notes
        );

        _assetRepositoryMock.Setup(r => r.AddAsync(It.Is<Asset>(a => a.AssetTag == command.AssetTag))).ReturnsAsync(fakeAsset);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(fakeAsset);
        _assetRepositoryMock.Verify(r => r.AddAsync(It.Is<Asset>(a => a.AssetTag == fakeAsset.AssetTag)), Times.Once);
    }

    [Fact]
    public async Task Handle_NullCommand_ThrowsArgumentNullEception()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
    }
}
