using Domain.Entities;
using MediatR;

namespace Application.Features.Assets.Commands.Create;

public record CreateAssetCommand(
    string Name,
    string AssetTag,
    string Serial,
    Guid? ModelId,
    Guid? StatusId,
    Guid? CompanyId,
    Guid? UserId,
    Guid? LocationId,
    Guid? SupplierId,
    Guid? DepreciationId,
    DateTime PurchaseDate,
    decimal PurchaseCost,
    string Notes
) : IRequest<Asset>;
