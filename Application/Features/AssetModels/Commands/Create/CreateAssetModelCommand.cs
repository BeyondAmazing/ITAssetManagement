using Domain.Entities;
using MediatR;

namespace Application.Features.AssetModels.Commands.Create;

public record CreateAssetModelCommand(string Name, Guid? ManufacturerId, Guid? CategoryId) : IRequest<AssetModel> { }
