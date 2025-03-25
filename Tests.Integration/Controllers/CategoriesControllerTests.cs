using Application.Features.Categories.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Tests.Common.Fakers;

namespace Tests.Integration.Controllers
{
    public class CategoriesControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IntegrationTestFixture _fixture;
        private readonly CategoryFaker _categoryFaker;

        public CategoriesControllerTests(IntegrationTestFixture fixture)
        {
            _fixture = fixture;
            _categoryFaker = new CategoryFaker();
            _categoryFaker.UseSeed(123);
        }

        [Fact]
        public async Task GetAllCategories_EmptyDatabase_ReturnsEmptyList()
        {
            // Act
            var response = await _fixture.Client.GetAsync("/api/v1/categories");

            // Assert
            response.EnsureSuccessStatusCode();
            var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
            categories.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateCategory_ValidRequest_ReturnsCreatedCategory()
        {
            // Arrange
            var fakeCategory = _categoryFaker.Generate();
            var command = new CreateCategoryCommand(fakeCategory.Name);

            // Act
            var response = await _fixture.Client.PostAsJsonAsync("/api/v1/categories", command);

            // Assert: Verify response and database
            response.EnsureSuccessStatusCode();
            var createdCategory = await response.Content.ReadFromJsonAsync<Category>();
            createdCategory.Should().NotBeNull();
            createdCategory!.Name.Should().Be(fakeCategory.Name);

            using var scope = _fixture.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DbCtx>();
            var dbCategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == command.Name);
            dbCategory.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateCategory_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var command = new CreateCategoryCommand(""); // Invalid input

            // Act
            var response = await _fixture.Client.PostAsJsonAsync("/api/v1/categories", command);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateCategory_NullRequest_ReturnsBadRequest()
        {
            // Act
            var response = await _fixture.Client.PostAsJsonAsync("/api/v1/categories", null as CreateCategoryCommand);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
