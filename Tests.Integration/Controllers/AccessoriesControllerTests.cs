using Application.Features.Accessories.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Tests.Common.Fakers;

namespace Tests.Integration.Controllers;

public class AccessoriesControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;
    private readonly AccessoryFaker _accessoryFaker = new AccessoryFaker();
    public AccessoriesControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _accessoryFaker.UseSeed(123);
    }

    [Fact]
    public async Task GetAllAccessories_EmptyDatabase_ReturnsEmptyList()
    {
        var response = await _fixture.Client.GetAsync("/api/v1/accessories");
        response.EnsureSuccessStatusCode();
        var accessories = await response.Content.ReadFromJsonAsync<List<Accessory>>();
        accessories.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateAccessory_ValidRequest_ReturnsCreatedAccessory()
    {
        var fakeAccessory = _accessoryFaker.Generate();
        var command = new CreateAccessoryCommand(
            fakeAccessory.Name,
            fakeAccessory.CategoryId,
            fakeAccessory.CompanyId,
            fakeAccessory.ManufacturerId,
            fakeAccessory.Quantity,
            fakeAccessory.PurchaseDate,
            fakeAccessory.PurchaseCost
        );

        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/accessories", command);
        response.EnsureSuccessStatusCode();
        var createdAccessory = await response.Content.ReadFromJsonAsync<Accessory>();
        createdAccessory.Should().NotBeNull();
        createdAccessory!.Name.Should().Be(fakeAccessory.Name);

        using var scope = _fixture.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DbCtx>();
        var dbAccessory = await dbContext.Accessories.FirstOrDefaultAsync(a => a.Name == fakeAccessory.Name);
        dbAccessory.Should().NotBeNull();
    }
}
