using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.AssetModels.Queries.GetAll;
public class GetAllAssetModelsQueryHandler : IRequestHandler<GetAllAssetModelsQuery, IEnumerable<AssetModel>>
{
    private readonly IAssetModelRepository _assetModelRepository;

    public GetAllAssetModelsQueryHandler(IAssetModelRepository assetModelRepository)
    {
        _assetModelRepository = assetModelRepository;
    }

    public async Task<IEnumerable<AssetModel>> Handle(GetAllAssetModelsQuery request, CancellationToken cancellationToken)
    {
        return await _assetModelRepository.GetAllAsync();
    }
}
