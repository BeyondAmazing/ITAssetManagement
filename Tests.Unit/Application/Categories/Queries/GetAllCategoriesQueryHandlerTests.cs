using Application.Features.Categories.Queries.GetAll;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Categories.Queries
{
    public class GetAllCategoriesQueryHandlerTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly GetAllCategoriesQueryHandler _handler;
        private readonly CategoryFaker _categoryFaker;

        public GetAllCategoriesQueryHandlerTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _handler = new GetAllCategoriesQueryHandler(_categoryRepositoryMock.Object);
            _categoryFaker = new CategoryFaker();
            _categoryFaker.UseSeed(123);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = _categoryFaker.Generate(5);
            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(5, result.Count());
            Assert.Equal(categories, result);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoCategoriesExist()
        {
            // Arrange
            var categories = new List<Category>();
            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _handler.Handle(new GetAllCategoriesQuery(), default);

            // Assert
            Assert.Empty(result);
        }
    }
}
