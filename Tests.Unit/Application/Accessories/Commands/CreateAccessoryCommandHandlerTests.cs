using Application.Features.Accessories.Commands.Create;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.Accessories.Commands;

public class CreateAccessoryCommandHandlerTests
{
    private readonly Mock<IAccessoryRepository> _accessoryRepositoryMock;
    private readonly CreateAccessoryCommandHandler _handler;
    private readonly AccessoryFaker _accessoryFaker = new AccessoryFaker();

    public CreateAccessoryCommandHandlerTests()
    {
        _accessoryRepositoryMock = new Mock<IAccessoryRepository>();
        _handler = new CreateAccessoryCommandHandler(_accessoryRepositoryMock.Object);
        _accessoryFaker.UseSeed(123);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesAccessory()
    {
        // Arrange
        var fakeAccessory = _accessoryFaker.Generate();
        var command = new CreateAccessoryCommand(
            fakeAccessory.Name, 
            fakeAccessory.CategoryId, 
            fakeAccessory.CompanyId, 
            fakeAccessory.ManufacturerId, 
            fakeAccessory.Quantity, 
            fakeAccessory.PurchaseDate, 
            fakeAccessory.PurchaseCost
        );
        _accessoryRepositoryMock.Setup(x => x.AddAsync(It.Is<Accessory>(a => a.Name == command.Name))).ReturnsAsync(fakeAccessory);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(fakeAccessory);
        _accessoryRepositoryMock.Verify(r => r.AddAsync(It.Is<Accessory>(a => a.Name == command.Name)), Times.Once);
    }

    [Fact]
    public async Task Handle_NullCommand_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
    }
}
