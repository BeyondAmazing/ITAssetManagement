using Domain.Entities;
using MediatR;

namespace Application.Features.Assets.Queries.GetHistory;

public record GetAssetHistoryQuery(Guid AssetId) : IRequest<IEnumerable<ActionLog>>;