using Domain.Entities;
using MediatR;

namespace Application.Features.Assets.Queries.ById;

public record GetAssetByIdQuery(Guid Id): IRequest<Asset?>;