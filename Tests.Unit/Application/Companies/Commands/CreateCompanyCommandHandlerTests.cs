using Application.Features.Companies.Commands.Create;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Companies.Commands;

public class CreateCompanyCommandHandlerTests
{
    private readonly Mock<ICompanyRepository> _companyRepositoryMock;
    private readonly CreateCompanyHandler _handler;
    private readonly CompanyFaker _companyFaker = new CompanyFaker();

    public CreateCompanyCommandHandlerTests()
    {
        // Setup
        _companyRepositoryMock = new Mock<ICompanyRepository>();
        _handler = new CreateCompanyHandler(_companyRepositoryMock.Object);
        _companyFaker.UseSeed(123);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesCompany()
    {
        // Arrange
        var expectedCompany = _companyFaker.Generate();
        var command = new CreateCompanyCommand(expectedCompany.Name, expectedCompany.Email, expectedCompany.Phone);

        _companyRepositoryMock.Setup(r => r.AddAsync(It.Is<Company>(c => c.Name == command.name))).ReturnsAsync(expectedCompany);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(expectedCompany.Name);
        _companyRepositoryMock.Verify(r => r.AddAsync(It.Is<Company>(c => c.Name == expectedCompany.Name)), Times.Once);
    }

    [Fact]
    public async Task Handle_NullCommand_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
    }
}
