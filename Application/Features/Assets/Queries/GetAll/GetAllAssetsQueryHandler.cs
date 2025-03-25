using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Assets.Queries.GetAll;

public class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQuery, IEnumerable<Asset>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAllAssetsQueryHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<IEnumerable<Asset>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
    {
        return await _assetRepository.GetAllAsync();
    }
}
