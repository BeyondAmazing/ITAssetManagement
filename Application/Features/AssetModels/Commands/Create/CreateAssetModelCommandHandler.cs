using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.AssetModels.Commands.Create;
public class CreateAssetModelCommandHandler : IRequestHandler<CreateAssetModelCommand, AssetModel>
{
    private readonly IAssetModelRepository _assetModelRepository;

    public CreateAssetModelCommandHandler(IAssetModelRepository assetModelRepository)
    {
        _assetModelRepository = assetModelRepository;
    }

    public async Task<AssetModel> Handle(CreateAssetModelCommand request, CancellationToken cancellationToken)
    {
        var assetModel = AssetModel.Create(request.Name, request.ManufacturerId, request.CategoryId);
        return await _assetModelRepository.AddAsync(assetModel);
    }
}
