using Application.Features.Licenses.Commands.Create;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Json;
using Tests.Common.Fakers;
using License = Domain.Entities.License;

namespace Tests.Integration.Controllers;

public class LicensesControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;
    private readonly LicenseFaker _licenseFaker;
    public LicensesControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _licenseFaker = new LicenseFaker();
        _licenseFaker.UseSeed(123);
    }

    [Fact]
    public async Task GetAllLicenses_EmptyDatabase_ReturnsEmptyList()
    {
        var response = await _fixture.Client.GetAsync("/api/v1/licenses");
        response.EnsureSuccessStatusCode();
        var licenses = await response.Content.ReadFromJsonAsync<List<License>>();
        licenses.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateLicense_ValidRequest_ReturnsCreatedLicense()
    {
        var fakeLicense = _licenseFaker.Generate();
        var command = new CreateLicenseCommand(
            fakeLicense.Name, fakeLicense.Serial, fakeLicense.Seats,
            fakeLicense.CompanyId, fakeLicense.SupplierId,
            fakeLicense.ExpirationDate, fakeLicense.PurchaseCost
        );

        var response = await _fixture.Client.PostAsJsonAsync("/api/v1/licenses", command);
        response.EnsureSuccessStatusCode();
        var createdLicense = await response.Content.ReadFromJsonAsync<License>();
        createdLicense.Should().NotBeNull();
        createdLicense!.Name.Should().Be(fakeLicense.Name);

        using var scope = _fixture.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DbCtx>();
        var dbLicense = await dbContext.Licenses.FirstOrDefaultAsync(l => l.Name == fakeLicense.Name);
        dbLicense.Should().NotBeNull();
    }
}
