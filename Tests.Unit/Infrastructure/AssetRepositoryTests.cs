using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tests.Unit.Extensions;

namespace Tests.Unit.Infrastructure;

public class AssetRepositoryTests
{
    private readonly Mock<DbCtx> _contextMock;
    private readonly Mock<DbSet<Asset>> _assetsMock;
    private readonly AssetRepository _repositoryMock;

    public AssetRepositoryTests()
    {
        _contextMock = new Mock<DbCtx>();
        _assetsMock = new Mock<DbSet<Asset>>();

        _contextMock.Setup(c => c.Assets).Returns(_assetsMock.Object);
        _repositoryMock = new AssetRepository(_contextMock.Object);
    }

    [Fact]
    public async Task GetByAssetTagAsync_ExistingTag_ReturnAsset()
    {
        // Arrange
        var asset = Asset.Create("Dell Precision 6990",
            "Laptop",
            "123456789",
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            DateTime.Today, (decimal)1200.00, string.Empty);

        var data = new List<Asset> { asset }.AsQueryable();
        _assetsMock.SetupDbSet(data);

        // Act
        var result = await _repositoryMock.GetByAssetTagAsync("Laptop");

        // Assert
        result.Should().NotBeNull();
        result!.AssetTag.Should().Be("Laptop");
    }

    [Fact]
    public async Task GetByAssetTagAsync_NonExistingTag_ReturnsNull()
    {
        // Arrange
        var data = new List<Asset>().AsQueryable();
        _assetsMock.SetupDbSet(data);

        // Act
        var result = await _repositoryMock.GetByAssetTagAsync("Laptop");

        // Assert
        result.Should().BeNull();
    }
}
