using Application.Features.Categories.Commands.Create;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Categories.Commands;

public class CreateCategoryCommandHandlerTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly CreateCategoryCommandHandler _handler;
    private readonly CategoryFaker _categoryFaker;

    public CreateCategoryCommandHandlerTests()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _handler = new CreateCategoryCommandHandler(_categoryRepositoryMock.Object);
        _categoryFaker = new CategoryFaker();
        _categoryFaker.UseSeed(123);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesCategory()
    {
        // Arrange
        var fakeCategory = _categoryFaker.Generate();
        var command = new CreateCategoryCommand(fakeCategory.Name);
 
        _categoryRepositoryMock.Setup(r => r.AddAsync(It.Is<Category>(c => c.Name == command.Name))).ReturnsAsync(fakeCategory);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(fakeCategory.Name);
        _categoryRepositoryMock.Verify(r => r.AddAsync(It.Is<Category>(c => c.Name == command.Name)), Times.Once);
    }

    [Fact]
    public async Task Handle_NullCommand_ThrowsArgumentNullException()
    {
        // Act & Assert: Test null command handling
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
    }
}
