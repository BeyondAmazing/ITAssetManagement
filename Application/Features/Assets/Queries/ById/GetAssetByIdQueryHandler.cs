using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Assets.Queries.ById;

public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, Asset?>
{
    private readonly IAssetRepository _assetRepository;

    public GetAssetByIdQueryHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<Asset?> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _assetRepository.GetByIdAsync(request.Id);
    }
}
