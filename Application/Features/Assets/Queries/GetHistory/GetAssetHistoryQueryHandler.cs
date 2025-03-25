using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Assets.Queries.GetHistory;

public class GetAssetHistoryQueryHandler : IRequestHandler<GetAssetHistoryQuery, IEnumerable<ActionLog>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAssetHistoryQueryHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<IEnumerable<ActionLog>> Handle(GetAssetHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _assetRepository.GetHistoryAsync(request.AssetId);
    }
}
