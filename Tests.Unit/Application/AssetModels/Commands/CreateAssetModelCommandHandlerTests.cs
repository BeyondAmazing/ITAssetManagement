using Application.Features.AssetModels.Commands.Create;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Tests.Common.Fakers;

namespace Tests.Unit.Application.AssetModels.Commands;

public class CreateAssetModelCommandHandlerTests
{
    private readonly Mock<IAssetModelRepository> _assetModelRepositoryMock;
    private readonly CreateAssetModelCommandHandler _handler;
    private readonly AssetModelFaker _assetModelFaker;

    public CreateAssetModelCommandHandlerTests()
    {
        _assetModelRepositoryMock = new Mock<IAssetModelRepository>();
        _handler = new CreateAssetModelCommandHandler(_assetModelRepositoryMock.Object);
        _assetModelFaker = new AssetModelFaker();
        _assetModelFaker.UseSeed(123);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesAssetModel()
    {
        // Arrange
        var assetModel = _assetModelFaker.Generate();
        var command = new CreateAssetModelCommand(assetModel.Name, assetModel.ManufacturerId.Value, assetModel.CategoryId.Value);
        _assetModelRepositoryMock.Setup(r => r.AddAsync(It.Is<AssetModel>(am => am.Name == command.Name))).ReturnsAsync(assetModel);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(assetModel.Name);
        _assetModelRepositoryMock.Verify(r => r.AddAsync(It.Is<AssetModel>(am => am.Name == assetModel.Name)), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsException()
    {
        // Arrange
        var command = new CreateAssetModelCommand(null, Guid.NewGuid(), Guid.NewGuid());

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ThrowsException()
    {
        // Arrange
        var assetModel = _assetModelFaker.Generate();
        var command = new CreateAssetModelCommand(assetModel.Name, assetModel.ManufacturerId.Value, assetModel.CategoryId.Value);
        _assetModelRepositoryMock.Setup(r => r.AddAsync(It.IsAny<AssetModel>())).ThrowsAsync(new Exception("Repository error"));

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Repository error");
    }
}
