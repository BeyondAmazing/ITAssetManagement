using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Tests.Unit.Infrastructure
{
    public class UserRepositoryTests : IClassFixture<InMemoryDatabaseFixture>
    {
        private readonly InMemoryDatabaseFixture _fixture;

        public UserRepositoryTests(InMemoryDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetByUsernameAsync_ExistingUser_ReturnsUser()
        {
            var CompanyId = Guid.NewGuid();
            // Arrange - Add a user to the in-memory database
            var user = User.Create("John_Doe", "John", "Doe", "john@example.com", CompanyId);
            _fixture.AddEntity(user);
            var repository = new UserRepository(_fixture.GetContext());

            // Act - Call the method to get the user by username
            var result = await repository.GetByUsernameAsync("John_Doe");

            // Assert - Verify the user is returned correctly
            result.Should().NotBeNull();
            result!.Username.Should().Be("John_Doe");
            result.FirstName.Should().Be("John");
            result.CompanyId.Should().Equals(CompanyId);
        }

        [Fact]
        public async Task GetByUsernameAsync_NonExistingUser_ReturnsNull()
        {
            // Arrange - Create repository with empty database
            var repository = new UserRepository(_fixture.GetContext());

            // Act - Try to get a non-existing user
            var result = await repository.GetByUsernameAsync("non_existent");

            // Assert - Verify null is returned
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_NewUser_AddsUser()
        {
            // Arrange - Create a new user and repository
            var companyId = Guid.NewGuid();
            var user = User.Create("John_Doe", "John", "Doe", "john@example.com", companyId);
            var repository = new UserRepository(_fixture.GetContext());

            // Act - Add the user
            var result = await repository.AddAsync(user);

            // Assert - Verify the user was added and returned
            result.Should().NotBeNull();
            result.Username.Should().Be("John_Doe");

            // Check the database directly to ensure presistance
            var dbUser = await _fixture.GetContext().Users.FirstOrDefaultAsync(u => u.Username == "John_Doe");
            dbUser.Should().NotBeNull();
        }
    }
}
