using Application.Features.Accessories.Commands.CheckOut;
using Domain.Interfaces;
using Moq;

namespace Tests.Unit.Application.Accessories.Commands;

public class CheckOutAccessoryCommandHandlerTests
{
    private readonly Mock<IAccessoryRepository> _accessoryRepositoryMock;
    private readonly CheckOutAccessoryCommandHandler _handler;

    public CheckOutAccessoryCommandHandlerTests()
    {
        _accessoryRepositoryMock = new Mock<IAccessoryRepository>();
        _handler = new CheckOutAccessoryCommandHandler(_accessoryRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_CallsCheckOutAsync()
    {
        var AccessoryId = Guid.NewGuid();
        var UserId = Guid.NewGuid();
        int Quantity = 1;
        // Arrange - Prepare test Data
        var command = new CheckOutAccessoryCommand(AccessoryId, UserId, Quantity);

        // No return value to mock since CheckOutAsync is void, just setup the call
        _accessoryRepositoryMock.Setup(r => r.CheckOutAsync(AccessoryId, UserId, Quantity)).Returns(Task.CompletedTask); // Simulate successful checkout

        // Act - Run the handler
        await _handler.Handle(command, CancellationToken.None);

        // Assert - Verify the repository method was called
        _accessoryRepositoryMock.Verify(r => r.CheckOutAsync(AccessoryId, UserId, Quantity), Times.Once); // Ensure CheckOutAsync was called with correct args
    }

    [Fact]
    public async Task Handle_NullCommand_ThrowsArgumentNullException()
    {
        // Step 2: Arrange - No setup needed beyond constructor

        // Step 3 & 4: Act & Assert - Expect an exception for null command
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
    }
}
