using Application.Features.Licenses.Queries.GetAll;
using Domain.Interfaces;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Licenses.Queries
{
    public class GetAllLicenseQueryHandlerTests
    {
        private readonly Mock<ILicenseRepository> _licenseRepository;
        private readonly GetAllLicensesQueryHandler _handler;
        private readonly LicenseFaker _licenseFaker = new LicenseFaker();

        public GetAllLicenseQueryHandlerTests()
        {
            _licenseRepository = new Mock<ILicenseRepository>();
            _handler = new GetAllLicensesQueryHandler(_licenseRepository.Object);
            _licenseFaker.UseSeed(123);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllLicenses()
        {
            // Arrange
            var licenses = _licenseFaker.Generate(5);
            _licenseRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(licenses);

            var query = new GetAllLicensesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(licenses.Count, result.Count());
            _licenseRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}
