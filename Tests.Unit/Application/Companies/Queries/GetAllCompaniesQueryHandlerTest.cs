using Application.Features.Companies.Queries.GetAll;
using Domain.Interfaces;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Companies.Queries
{
    public class GetAllCompaniesQueryHandlerTest
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly GetAllCompaniesQueryHandler _handler;
        private readonly CompanyFaker _companyFaker;

        public GetAllCompaniesQueryHandlerTest()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _handler = new GetAllCompaniesQueryHandler(_companyRepositoryMock.Object);
            _companyFaker = new CompanyFaker();
            _companyFaker.UseSeed(123);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllCompanies()
        {
            // Arrange
            var companies = _companyFaker.Generate(10);
            _companyRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(companies);

            var query = new GetAllCompaniesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Count());
            Assert.Equal(companies, result);
        }
    }
}
