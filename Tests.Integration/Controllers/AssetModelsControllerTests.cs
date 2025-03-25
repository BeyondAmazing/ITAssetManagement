using Application.Features.AssetModels.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Tests.Common.Fakers;

namespace Tests.Integration.Controllers;

public class AssetModelsControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;
    private readonly AssetModelFaker _assetModelFaker;
    private readonly ManufacturerFaker _manufacturerFaker;
    private readonly CategoryFaker _categoryFaker;

    public AssetModelsControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _assetModelFaker = new AssetModelFaker();
        _assetModelFaker.UseSeed(123);
        _manufacturerFaker = new ManufacturerFaker();
        _manufacturerFaker.UseSeed(123);
        _categoryFaker = new CategoryFaker();
        _categoryFaker.UseSeed(123);
    }

    [Fact]
    public async Task CreateAssetModel_ValidRequest_ReturnsCreatedAssetModel()
    {
        var assetModel = _assetModelFaker.Generate();
        var manufacturer = _manufacturerFaker.Generate();
        var category = _categoryFaker.Generate();

        // Arrange
        var command = new CreateAssetModelCommand(assetModel.Name, manufacturer.Id, category.Id);

        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/assetmodels", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdAssetModel = await response.Content.ReadFromJsonAsync<AssetModel>();
        createdAssetModel.Should().NotBeNull();
        createdAssetModel!.Name.Should().Be(assetModel.Name);

        using var scope = _fixture.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DbCtx>();
        var dbAssetModel = await dbContext.AssetModels.FirstOrDefaultAsync(am => am.Name == assetModel.Name);
        dbAssetModel.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateAssetModel_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var command = new CreateAssetModelCommand("", Guid.Empty, Guid.Empty);

        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/assetmodels", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateAssetModel_NullInput_ReturnsBadRequest()
    {
        // Arrange
        CreateAssetModelCommand command = null;

        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/assetmodels", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
