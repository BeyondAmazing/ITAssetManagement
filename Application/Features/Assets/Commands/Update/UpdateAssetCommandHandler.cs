using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Assets.Commands.Update;

public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, Asset>
{
    private readonly IAssetRepository _assetRepository;
    private readonly IMapper _mapper;

    public UpdateAssetCommandHandler(IAssetRepository assetRepository, IMapper mapper)
    {
        _assetRepository = assetRepository;
        _mapper = mapper;
    }

    public async Task<Asset> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _assetRepository.GetByIdAsync(request.Id);
        if (asset == null) throw new Exception("Asset not found");

        // Update properties
        //asset = Asset.Create(
        //    request.Name, request.AssetTag, request.Serial, request.ModelId,
        //    request.StatusId, request.CompanyId, request.UserId, request.LocationId,
        //    request.SupplierId, request.DepreciationId, request.PurchaseDate,
        //    request.PurchaseCost, request.Notes);
        asset = _mapper.Map<Asset>(request);
        asset.Id = request.Id; // Preserve ID

        await _assetRepository.UpdateAsync(asset);
        return asset;
    }
}
