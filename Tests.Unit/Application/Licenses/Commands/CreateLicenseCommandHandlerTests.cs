using Application.Features.Licenses.Commands.Create;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;
using License = Domain.Entities.License;

namespace Tests.Unit.Application.Licenses.Commands;

public class CreateLicenseCommandHandlerTests
{
    private readonly Mock<ILicenseRepository> _licenseRepositoryMock;
    private readonly CreateLicenseCommandHandler _handler;
    private readonly LicenseFaker _licenseFaker = new LicenseFaker();

    public CreateLicenseCommandHandlerTests()
    {
        _licenseRepositoryMock = new Mock<ILicenseRepository>();
        _handler = new CreateLicenseCommandHandler(_licenseRepositoryMock.Object);
        _licenseFaker.UseSeed(123);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesLicense()
    {
        var fakeLicense = _licenseFaker.Generate();
        // Arrange
        var command = new CreateLicenseCommand(
            fakeLicense.Name,
            fakeLicense.Serial,
            fakeLicense.Seats,
            fakeLicense.CompanyId,
            fakeLicense.SupplierId,
            fakeLicense.ExpirationDate,
            fakeLicense.PurchaseCost
        );
        var expectedLicense = License.Create(
            fakeLicense.Name,
            fakeLicense.Serial,
            fakeLicense.Seats,
            fakeLicense.CompanyId,
            fakeLicense.SupplierId,
            fakeLicense.ExpirationDate,
            fakeLicense.PurchaseCost
        );
        _licenseRepositoryMock.Setup(r => r.AddAsync(It.Is<License>(l => l.Name == command.Name))).ReturnsAsync(expectedLicense);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(fakeLicense);
        _licenseRepositoryMock.Verify(r => r.AddAsync(It.Is<License>(l => l.Name == command.Name)), Times.Once);
    }
}
