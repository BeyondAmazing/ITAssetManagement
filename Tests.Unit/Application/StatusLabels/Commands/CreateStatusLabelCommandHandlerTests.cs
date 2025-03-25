using Application.Features.StatusLabels.Commands.Create;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.StatusLabels.Commands;

public class CreateStatusLabelCommandHandlerTests
{
    private readonly Mock<IStatusLabelRepository> _statusLabelsRepositoryMock;
    private readonly CreateStatusLabelCommandHandler _handler;
    private readonly StatusLabelFaker _statusLabelFaker;

    public CreateStatusLabelCommandHandlerTests()
    {
        _statusLabelsRepositoryMock = new Mock<IStatusLabelRepository>();
        _handler = new CreateStatusLabelCommandHandler(_statusLabelsRepositoryMock.Object);
        _statusLabelFaker = new StatusLabelFaker();
        _statusLabelFaker.UseSeed(123);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesStatusLabel()
    {
        // Arrange
        var statusLabel = _statusLabelFaker.Generate();
        var command = new CreateStatusLabelCommand(statusLabel.Name, statusLabel.Deployable, statusLabel.Pending, statusLabel.Archived);
        _statusLabelsRepositoryMock.Setup(r => r.AddAsync(It.Is<StatusLabel>(l => l.Name == command.Name))).ReturnsAsync(statusLabel);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(statusLabel.Name);
        result.Deployable.Should().Be(statusLabel.Deployable);
        _statusLabelsRepositoryMock.Verify(r => r.AddAsync(It.Is<StatusLabel>(s => s.Name == statusLabel.Name)), Times.Once);
    }

    [Fact]
    public async Task Handle_NullCommand_ThrowsArgumentNullException()
    {
        // Act & Assert: Test null command
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
    }
}
