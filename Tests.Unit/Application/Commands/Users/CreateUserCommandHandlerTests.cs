using Application.Features.Users.Commands.Create;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Tests.Unit.Application.Commands.Users;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>(); // We mock the IUserRepository to simulate database behavior
        _handler = new CreateUserCommandHandler(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesUser()
    {
        // Arrange - Prepare the Test Data and Mock behavior
        var command = new CreateUserCommand("john_doe", "John", "Doe", "john@example.com", null);
        var expectedUser = User.Create(
            "john_doe",
            "John",
            "Doe",
            "john@example.com",
            null
        );

        // Tell the mock what to do when the AddAsync is called: return our expected user
        _userRepositoryMock.Setup(r => r.AddAsync(It.Is<User>(u => u.Username == command.Username))).ReturnsAsync(expectedUser);

        // Act - Run the handler with the command
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert - Verify the result and mock interaction
        result.Should().NotBeNull(); // Ensure we got a user back
        result.Username.Should().Be("john_doe"); // Check the username matches
        result.FirstName.Should().Be("John Doe"); // Check the name matches
        _userRepositoryMock.Verify(r => r.AddAsync(It.Is<User>(u => u.Username == "john_doe")), Times.Once); // Ensure AddAsync was called once
    }

    [Fact]
    public async Task Handle_NullCommand_ThrowsArgumentNullException()
    {
        // Arrange - No need for extra setup, just testing for null input
        // Act & Assert - Expect an exception when command is null
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
    }
}
