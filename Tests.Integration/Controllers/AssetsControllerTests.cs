using Application.Features.Assets.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Tests.Common.Fakers;

namespace Tests.Integration.Controllers
{
    public class AssetsControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IntegrationTestFixture _fixture;
        private readonly AssetFaker _assetFaker;

        public AssetsControllerTests(IntegrationTestFixture fixture)
        {
            _fixture = fixture;
            _assetFaker = new AssetFaker();
            _assetFaker.UseSeed(123);
        }

        [Fact]
        public async Task GetAllAssets_EmptyDatabase_ReturnEmptyList()
        {
            // Act
            var response = await _fixture.Client.GetAsync("/api/v1/assets");
            response.EnsureSuccessStatusCode();

            var assets = await response.Content.ReadFromJsonAsync<List<Asset>>();
            assets.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateAsset_ValidRequest_ReturnsCreatedAsset()
        {
            // Arrange
            var asset = _assetFaker.Generate();
            var command = new CreateAssetCommand(
                asset.Name, 
                asset.AssetTag, 
                asset.Serial, 
                asset.ModelId,
                asset.StatusId,
                asset.CompanyId, 
                asset.UserId, 
                asset.LocationId, 
                asset.SupplierId,
                asset.DepreciationId,
                asset.PurchaseDate, 
                asset.PurchaseCost, 
                asset.Notes
            );

            // Act
            var response = await _fixture.Client.PostAsJsonAsync("/api/v1/assets", command);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdAsset = await response.Content.ReadFromJsonAsync<Asset>();
            createdAsset.Should().NotBeNull();
            createdAsset.AssetTag.Should().Be(asset.AssetTag);

            // Verify in Db
            using var scope = _fixture.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DbCtx>();
            var dbAsset = await dbContext.Assets.FirstOrDefaultAsync(a => a.AssetTag == asset.AssetTag);
            dbAsset.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateAsset_DuplicateAssetTag_ReturnsConflict()
        {
            // Arrange
            var asset = _assetFaker.Generate();
            var command = new CreateAssetCommand(
                asset.Name,
                asset.AssetTag,
                asset.Serial,
                asset.ModelId,
                asset.StatusId,
                asset.CompanyId,
                asset.UserId,
                asset.LocationId,
                asset.SupplierId,
                asset.DepreciationId,
                asset.PurchaseDate,
                asset.PurchaseCost,
                asset.Notes
            );

            // Act
            await _fixture.Client.PostAsJsonAsync("/api/v1/assets", command);
            var response = await _fixture.Client.PostAsJsonAsync("/api/v1/assets", command);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task GetAssetById_ValidId_ReturnsAsset()
        {
            // Arrange
            var asset = _assetFaker.Generate();
            var command = new CreateAssetCommand(
                asset.Name,
                asset.AssetTag,
                asset.Serial,
                asset.ModelId,
                asset.StatusId,
                asset.CompanyId,
                asset.UserId,
                asset.LocationId,
                asset.SupplierId,
                asset.DepreciationId,
                asset.PurchaseDate,
                asset.PurchaseCost,
                asset.Notes
            );
            var createResponse = await _fixture.Client.PostAsJsonAsync("/api/v1/assets", command);
            var createdAsset = await createResponse.Content.ReadFromJsonAsync<Asset>();

            // Act
            var response = await _fixture.Client.GetAsync($"/api/v1/assets/{createdAsset.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var fetchedAsset = await response.Content.ReadFromJsonAsync<Asset>();
            fetchedAsset.Should().NotBeNull();
            fetchedAsset.Id.Should().Be(createdAsset.Id);
        }

        [Fact]
        public async Task GetAssetById_InvalidId_ReturnsNotFound()
        {
            // Act
            var response = await _fixture.Client.GetAsync("/api/v1/assets/999");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
