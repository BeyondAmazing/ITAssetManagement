using Domain.Entities;
using MediatR;

namespace Application.Features.AssetModels.Queries.GetAll;

public record GetAllAssetModelsQuery : IRequest<IEnumerable<AssetModel>> { }
