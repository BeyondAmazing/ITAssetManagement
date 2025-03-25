using Domain.Interfaces;
using MediatR;

namespace Application.Features.Assets.Commands.CheckInOut;

class CheckOutAssetCommandHandler : IRequestHandler<CheckOutAssetCommand>
{
    private IAssetRepository _assetRepository;

    public CheckOutAssetCommandHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task Handle(CheckOutAssetCommand request, CancellationToken cancellationToken)
    {
        await _assetRepository.CheckOutAsync(request.AssetId, request.UserId);
    }
}
