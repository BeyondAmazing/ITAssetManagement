using Application.Features.Companies.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Tests.Common.Fakers;

namespace Tests.Integration.Controllers
{
    public class CompaniesControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IntegrationTestFixture _fixture;
        private readonly CompanyFaker _companyFaker;

        public CompaniesControllerTests(IntegrationTestFixture fixture)
        {
            _fixture = fixture;
            _companyFaker = new CompanyFaker();
            _companyFaker.UseSeed(123);
        }

        [Fact]
        public async Task GetAllCompanies_EmptyDatabase_ReturnsEmptyList()
        {
            var response = await _fixture.Client.GetAsync("/api/v1/companies");
            response.EnsureSuccessStatusCode();
            var companies = await response.Content.ReadFromJsonAsync<List<Company>>();
            companies.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateCompany_ValidRequest_ReturnsCreatedCompany()
        {
            var fakeCompany = _companyFaker.Generate();
            var command = new CreateCompanyCommand(fakeCompany.Name, fakeCompany.Email, fakeCompany.Phone);

            var response = await _fixture.Client.PostAsJsonAsync("/api/v1/companies", command);
            response.EnsureSuccessStatusCode();
            var createdCompany = await response.Content.ReadFromJsonAsync<Company>();
            createdCompany.Should().NotBeNull();
            createdCompany!.Name.Should().Be(fakeCompany.Name);

            using var scope = _fixture.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DbCtx>();
            var dbCompany = await dbContext.Companies.FirstOrDefaultAsync(c => c.Name == fakeCompany.Name);
            dbCompany.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateCompany_InvalidRequest_ReturnsBadRequest()
        {
            var command = new CreateCompanyCommand("", "", "");

            var response = await _fixture.Client.PostAsJsonAsync("/api/v1/companies", command);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateCompany_NullRequest_ReturnsBadRequest()
        {
            CreateCompanyCommand command = null;

            var response = await _fixture.Client.PostAsJsonAsync("/api/v1/companies", command);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
